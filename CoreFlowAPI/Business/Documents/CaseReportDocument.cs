using CoreFlowSharedLibrary.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CoreFlowAPI.Business.Documents
{
    public class CaseReportDocument : IDocument
    {
        private readonly CaseDTO _case;
        private readonly List<SystemAccessDTO> _accounts;

        public CaseReportDocument(CaseDTO caseDto, List<SystemAccessDTO> accounts)
        {
            _case = caseDto;
            _accounts = accounts;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(t => t.FontFamily("Arial").FontSize(11).FontColor("#2c2c2c"));

                page.Content().Column(col =>
                {
                    ComposeHeader(col);
                    ComposeEmployeeSection(col);
                    ComposeAccessSection(col);
                    ComposeSignatureSection(col);
                });
            });
        }

        private void ComposeHeader(ColumnDescriptor col)
        {
            col.Item().BorderBottom(2).BorderColor("#444444").PaddingBottom(10).Row(row =>
            {
                row.RelativeItem().Text(_case.Employee.Company.ToString())
                    .FontSize(20).Bold();
                row.RelativeItem().AlignRight().Column(c =>
                {
                    c.Item().Text($"Datum: {DateTime.Now:yyyy-MM-dd}").FontSize(12);
                    c.Item().Text("Dokument: Behörighetsrapport").FontSize(12);
                });
            });
        }

        private void ComposeEmployeeSection(ColumnDescriptor col)
        {
            col.Item().PaddingTop(25).Column(section =>
            {
                SectionTitle(section, "Anställd");

                section.Item().PaddingTop(10).Grid(grid =>
                {
                    grid.Columns(2);
                    grid.Spacing(10);

                    InfoCell(grid, "Namn", $"{_case.Employee.FirstName} {_case.Employee.LastName}");
                    InfoCell(grid, "Avdelning", _case.Employee.Department ?? "");
                    InfoCell(grid, "Titel", _case.Employee.Title ?? "");
                    InfoCell(grid, "Mobilnr", _case.Employee.PhoneNumber ?? "");
                });
            });
        }

        private void ComposeAccessSection(ColumnDescriptor col)
        {
            col.Item().PaddingTop(25).Column(section =>
            {
                SectionTitle(section, "Systembehörigheter");

                section.Item().PaddingTop(10).Table(table =>
                {
                    table.ColumnsDefinition(c =>
                    {
                        c.RelativeColumn();
                        c.ConstantColumn(80);
                    });

                    table.Header(h =>
                    {
                        h.Cell().Background("#f0f0f0").Padding(8).Text("System").Bold();
                        h.Cell().Background("#f0f0f0").Padding(8).Text("Status").Bold();
                    });

                    foreach (var access in _accounts)
                    {
                        table.Cell().Border(1).BorderColor("#dddddd").Padding(8).Text(access.Name ?? "");
                        table.Cell().Border(1).BorderColor("#dddddd").Padding(8).Text("☐");
                    }
                });
            });
        }

        private void ComposeSignatureSection(ColumnDescriptor col)
        {
            col.Item().PaddingTop(25).Column(section =>
            {
                SectionTitle(section, "Godkännande");
                section.Item().PaddingTop(30).Text("___________________________");
                section.Item().Text("Signatur");
            });
        }

        private static void SectionTitle(ColumnDescriptor section, string title)
        {
            section.Item().BorderLeft(4).BorderColor("#0078D4").PaddingLeft(8)
                .Text(title).FontSize(16).Bold();
        }

        private static void InfoCell(GridDescriptor grid, string label, string value)
        {
            grid.Item().Column(c =>
            {
                c.Item().Text(label).Bold().FontColor("#555555");
                c.Item().Text(value);
            });
        }
    }
}
