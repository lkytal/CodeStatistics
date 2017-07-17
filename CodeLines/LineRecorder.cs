using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLines
{
	class LineRecorder
	{
		public readonly string path;
		public readonly List<string> ext;
		public int codeLines { private set; get; } = 0;
		public int emptyLines { private set; get; } = 0;
		public int totalLines => emptyLines + codeLines;

		private FileFinder fileFinder;

		public LineRecorder(string _path, string _ext)
		{
			path = _path;
			ext = new List<string>(_ext.Split(','));
		}

		public static (int code, int empty) CountFile(string path)
		{
			int codeCount = 0, emptyCount = 0;
			var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
			var reader = new StreamReader(fs);
			reader.BaseStream.Seek(0, SeekOrigin.Begin);

			while (reader.Peek() > 0)
			{
				string strLine = reader.ReadLine();

				if (String.IsNullOrWhiteSpace(strLine))
				{
					emptyCount += 1;
				}
				else
				{
					codeCount += 1;
				}
			}

			return (codeCount, emptyCount);
		}

		public void Compute()
		{
			fileFinder = new FileFinder(path);

			fileFinder.map(CountProc);
		}

		public void CountProc(FileInfo file)
		{
			foreach (var item in ext)
			{
				if (file.Extension == item)
				{
					(int code, int empty) = CountFile(file.FullName);

					codeLines += code;
					emptyLines += empty;

					return;
				}
			}
		}
	}

	public class FileFinder
	{
		public delegate void mapProc(FileInfo file);

		private readonly string path;

		public FileFinder(string _path)
		{
			path = _path;
		}

		public void map(mapProc proc)
		{
			doMap(path, proc);
		}

		public void doMap(string currentPath, mapProc proc)
		{
			DirectoryInfo currentFolder = new DirectoryInfo(currentPath);

			foreach (var folder in currentFolder.GetDirectories())
			{
				doMap(folder.FullName, proc);
			}

			foreach (var file in currentFolder.GetFiles())
			{
				proc(file);
			}
		}
	}
}
