using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeLines;

namespace UnitTest
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		private void OnCompute()
		{
			const string path = @"D:\Code\C#\CodeLines\";

			FileFinder f = new FileFinder(path);

			f.map(output);
		}

		private void output(FileInfo file)
		{
			Debug.WriteLine(file.FullName);
		}
	}
}
