using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace FINAL_DotNet
{
    internal sealed class FrmXemBaoCao : Form
    {
        private readonly ReportViewer reportViewer;

        public FrmXemBaoCao(CauHinhBaoCao cauHinh)
        {
            if (cauHinh == null) throw new ArgumentNullException(nameof(cauHinh));

            Text = cauHinh.TieuDeCuaSo;
            StartPosition = FormStartPosition.CenterParent;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(900, 650);
            BackColor = Color.White;

            reportViewer = new ReportViewer
            {
                Dock = DockStyle.Fill,
                ProcessingMode = ProcessingMode.Local,
                ShowBackButton = false,
                ShowFindControls = true,
                ShowPageNavigationControls = true,
                ShowPrintButton = true,
                ShowRefreshButton = false,
                ShowStopButton = false,
                ShowZoomControl = true
            };
            Controls.Add(reportViewer);

            reportViewer.LocalReport.ReportEmbeddedResource = BaoCaoService.TaiNguyenMauBaoCao;
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dsDongBaoCao", cauHinh.CacDong));
            reportViewer.LocalReport.SetParameters(cauHinh.ThamSo);
            Load += FrmXemBaoCao_Load;
        }

        private void FrmXemBaoCao_Load(object sender, EventArgs e)
        {
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.PageWidth;
            reportViewer.RefreshReport();
        }
    }
}
