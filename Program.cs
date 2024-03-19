// See https://aka.ms/new-console-template for more information
using System.Reflection;
using Test_IFC;
using Test_IFC.Properties;
using Xbim.Ifc;
using Xbim.Ifc2x3.SharedBldgElements;
using Xbim.Ifc4.Interfaces;

File.WriteAllBytes("Тында_в_координатах.ifc", Resources.Тында_в_координатах);
using (var model = IfcStore.Open(Environment.CurrentDirectory + @"\Тында_в_координатах.ifc"))
{
    var firstWall = model.Instances.FirstOrDefault<IfcBeam>();
    var ifcProject = model.FederatedInstances.OfType<IIfcProject>().FirstOrDefault();
    var ifcWalls = model.FederatedInstances.OfType<IIfcWall>();
    var properties = ifcWalls.First(x => x.GlobalId == "2YLo7mDY5O93xbPB6rTIrr").IsDefinedBy;
    //var allQuanity = properties
    //    .Where(r => r.RelatingPropertyDefinition is IIfcElementQuantity)
    //    .SelectMany(q => ((IIfcElementQuantity)q.RelatingPropertyDefinition).Quantities
    //    .OfType<IIfcElementQuantity>());
    //.SelectMany(t => t.GetType().GetProperties()
    //.Where(p => p.Name.Contains("Value"))
    //.Select(z => new Property(z.Name, z.GetValue(t)))));
    //var singleProperty = properties
    //  .Where(r => r.RelatingPropertyDefinition is IIfcPropertySet)
    //  .SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
    //  .OfType<IIfcPropertySingleValue>()
    //  .Select(x => new Property(x.Name.ToString(), x.NominalValue.Value));
    var allProperty = properties
        .Where(r => r.RelatingPropertyDefinition is IIfcElementQuantity)
        .SelectMany(q => ((IIfcElementQuantity)q.RelatingPropertyDefinition).Quantities
            .SelectMany(t => t.GetType().GetProperties()
                .Where(p => p.Name.Contains("Value"))
                .Select(z => new Property(t.Name, z.GetValue(t)))))
        .Union(properties
            .Where(r => r.RelatingPropertyDefinition is IIfcPropertySet)
            .SelectMany(r => ((IIfcPropertySet)r.RelatingPropertyDefinition).HasProperties)
                .OfType<IIfcPropertySingleValue>()
                .Select(x => new Property(x.Name.ToString(), x.NominalValue.Value)));
    foreach (var item in allProperty)
    {
        Console.WriteLine(item.ToString());
    }
    //var hz =
    //    model.FederatedInstances.OfType<IIfcWall>()
    //    .Select(x => x.IsDefinedBy
    //        .Select(x => x.RelatingPropertyDefinition) // Для всех стен
    //        .OfType<IfcElementQuantity>()
    //        .SelectMany(q => q.Quantities
    //            .SelectMany(x => x.GetType().GetProperties()
    //                .Where(p => p.Name.Contains("Value"))
    //                .Select(y => (y.GetValue(x), y.Name)))));
};

Console.WriteLine("Hello, World!");
