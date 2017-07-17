using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using Microsoft.Win32;

namespace CodeLines
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnChoose(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog
			{
				RootFolder = Environment.SpecialFolder.MyComputer
			};

			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				PathBox.Text = dialog.SelectedPath;
			}
		}

		private void OnCompute(object sender, RoutedEventArgs e)
		{
			string path = PathBox.Text;

			LineRecorder l = new LineRecorder(path, ".cs,.xaml,.c,.cpp,.h,.pas,.frm,.java,.html,.js,.css");
			l.Compute();

			var format = $"Total: {l.totalLines} \nCode: {l.codeLines} \n" + 
				$"Empty: {l.emptyLines} \nChar Nums: {l.codeNums}";
			Rst.AppendText(format);
		}
	}
}
