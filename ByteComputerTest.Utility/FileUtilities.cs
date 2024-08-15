using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteComputerTest.Utility
{
    public class FileUtilities
    {
        //This method will convert the uploaded file into byte array
        private byte[] ConvertToByteArray(string filePath)
        {
            byte[] fileData;
            //Create a File Stream Object to read the data
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    fileData = reader.ReadBytes((int)fs.Length);
                }
            }
            return fileData;
        }
    }
}
