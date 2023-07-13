using Microsoft.VisualBasic.FileIO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Catalog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string categoriesCsv = "C:\\Users\\JesúsAller\\Desktop\\Catalog\\Catalog\\input\\Categories.csv";
            string productsCsv = "C:\\Users\\JesúsAller\\Desktop\\Catalog\\Catalog\\input\\Products.csv";
            List<Producto> productos = new List<Producto>();
            List<Category> catalogos = new List<Category>();

            using (var parser = new TextFieldParser(productsCsv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                while (!parser.EndOfData)
                {
                    if (parser.LineNumber == 1)
                    {
                        parser.ReadFields();
                        continue;
                    }

                    string[] fields = parser.ReadFields();

                    productos.Add(new Producto
                    {
                        Id = int.Parse(fields[0]),
                        CategoryId = int.Parse(fields[1]),
                        Name = fields[2],
                        Price = float.Parse(fields[3])
                    });
                }
            }

            using (var parser = new TextFieldParser(categoriesCsv))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                while (!parser.EndOfData)
                {
                    if (parser.LineNumber == 1)
                    {
                        parser.ReadFields();
                        continue;
                    }

                    string[] fields = parser.ReadFields();
                    catalogos.Add(new Category
                    {
                        Id = int.Parse(fields[0]),
                        Name = fields[1],
                        Description = fields[2],
                        Productos = productos.Where(x => x.CategoryId == int.Parse(fields[0])).ToArray()
                    });
                }
            }

            using (var writer = new StreamWriter("C:\\Users\\JesúsAller\\Desktop\\Catalog\\Catalog\\output\\Catalog.json"))
            {
                writer.WriteLine(JsonSerializer.Serialize(catalogos));
            }

            using (var writer = new StreamWriter("C:\\Users\\JesúsAller\\Desktop\\Catalog\\Catalog\\output\\Catalog.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Category[]));
                using (StringWriter sWriter = new StringWriter())
                {
                    serializer.Serialize(sWriter, catalogos.ToArray());
                    writer.WriteLine(sWriter.ToString());
                }
            }

        }
    }
}