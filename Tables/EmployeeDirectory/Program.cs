// Gallery: "Table". Builds a table from a list of records.
using DOM;
using DOM.Elements.TableElements;
using ExampleSupport;

var employees = new[]
{
    new Employee("Ada Lovelace", "Engineering", "Lead Developer"),
    new Employee("Grace Hopper", "Engineering", "Architect"),
    new Employee("Mary Jackson", "Operations", "Analyst"),
};

using var doc = new VDocs();
doc.LicensePath = ExampleLicense.Path;

// #region manual-table
var table = new Table(employees.Length + 1, 3);

string[] headers = { "Name", "Department", "Role" };
for (int c = 0; c < headers.Length; c++)
    table[0, c].AddText(headers[c]);

for (int r = 0; r < employees.Length; r++)
{
    table[r + 1, 0].AddText(employees[r].Name);
    table[r + 1, 1].AddText(employees[r].Department);
    table[r + 1, 2].AddText(employees[r].Role);
}

doc.AddTable(table);
// #endregion

doc.AddParagraph();

// #region from-enumerable
// Or let VDocs generate the rows, plus a header row, from the record's properties:
doc.AddTable(Table.FromEnumerable(employees, includeHeaders: true));
// #endregion

doc.SaveAsWord(ExampleOutput.PathFor("directory.docx"));
doc.SaveAsPdf(ExampleOutput.PathFor("directory.pdf"));

public record Employee(string Name, string Department, string Role);
