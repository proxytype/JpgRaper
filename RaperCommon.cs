using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JpgRaperLib
{
    public class RaperCommon
    {
        public enum SUPPORT_SIGNATURE
        {
            UNKNOWN = -1,
            JPG,
            ZIP,
            RAR,
            EXE,
        }

        //https://en.wikipedia.org/wiki/List_of_file_signatures
        public readonly byte[] SIGNATURE_RAR = { 0x52, 0x61, 0x72, 0x21, 0x1a, 0x07 };
        //PK - ZIP
        public readonly byte[] SIGNATURE_ZIP = { 0x50, 0x4b, 0x3, 0x4 };
        //JPEG RAW
        public readonly byte[] SIGNATURE_JPG_RAW = { 0xff, 0xd8, 0xff, 0xdb };
        //JPEG JFIF FF D8 FF E0 00 10 4A 46 49 46 00 01
        public readonly byte[] SIGNATURE_JPG_JFIF = { 0xff, 0xd8, 0xff, 0xe0 };
        //JPEG JFIF (another version?) FF D8 FF EE
        public readonly byte[] SIGNATURE_JPG_JFIF2 = { 0xff, 0xd8, 0xff, 0xee };
        //JPEG EXIF FF D8 FF E1 ?? ?? 45 78 69 66 00 00
        public readonly byte[] SIGNATURE_JPG_EXIF = { 0xff, 0xd8, 0xff, 0xe1 };

        public SUPPORT_SIGNATURE getFileType(string file)
        {

            using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
            {
                int read;
                byte[] buffer = new byte[2];

                read = reader.Read(buffer, 0, buffer.Length);

                if (buffer.SequenceEqual(new byte[] { SIGNATURE_ZIP[0], SIGNATURE_ZIP[1] }))
                {
                    read = reader.Read(buffer, 0, buffer.Length);
                    if (buffer.SequenceEqual(new byte[] { SIGNATURE_ZIP[2], SIGNATURE_ZIP[3] }))
                    {
                        return SUPPORT_SIGNATURE.ZIP;
                    }
                }

                if (buffer.SequenceEqual(new byte[] { SIGNATURE_RAR[0], SIGNATURE_RAR[1] }))
                {
                    read = reader.Read(buffer, 0, buffer.Length);

                    if (buffer.SequenceEqual(new byte[] { SIGNATURE_RAR[2], SIGNATURE_RAR[3] }))
                    {
                        read = reader.Read(buffer, 0, buffer.Length);

                        if (buffer.SequenceEqual(new byte[] { SIGNATURE_RAR[4], SIGNATURE_RAR[5] }))
                        {
                            return SUPPORT_SIGNATURE.RAR;
                        }
                    }

                }

                if (buffer.SequenceEqual(new byte[] { SIGNATURE_JPG_RAW[0], SIGNATURE_JPG_RAW[1] }))
                {
                    read = reader.Read(buffer, 0, buffer.Length);

                    if (buffer.SequenceEqual(new byte[] { SIGNATURE_JPG_RAW[2], SIGNATURE_JPG_RAW[3] }))
                    {
                        return SUPPORT_SIGNATURE.JPG;
                    }
                    else if (buffer.SequenceEqual(new byte[] { SIGNATURE_JPG_JFIF[2], SIGNATURE_JPG_JFIF[3] }))
                    {
                        return SUPPORT_SIGNATURE.JPG;
                    }
                    else if (buffer.SequenceEqual(new byte[] { SIGNATURE_JPG_JFIF2[2], SIGNATURE_JPG_JFIF2[3] }))
                    {
                        return SUPPORT_SIGNATURE.JPG;
                    }
                    else if (buffer.SequenceEqual(new byte[] { SIGNATURE_JPG_EXIF[2], SIGNATURE_JPG_EXIF[3] }))
                    {
                        return SUPPORT_SIGNATURE.JPG;
                    }

                }

                return SUPPORT_SIGNATURE.UNKNOWN;
            }


        }
    }
}
