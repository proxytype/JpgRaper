# JpgRaper
By hiding compressed files inside a JPEG, we can take advantage of the fact that most JPEG encoders only analyze the image stream and ignore any additional information. This enables us to inject files into a JPEG and maintain its image-like behavior on most systems.

# Signature Support
```csharp
//https://en.wikipedia.org/wiki/List_of_file_signatures
//RAR
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
```

#Example

```csharp
writer.writeJpg(@"C:\Users\proxytype\Desktop\JpgRaperTest\Images\1.jpg", @"C:\Users\proxytype\Desktop\JpgRaperTest\archives\1.rar", @"C:\Users\proxytype\Desktop\JpgRaperTest\output.jpg", true);
            
reader.readJpg(@"C:\Users\proxytype\Desktop\JpgRaperTest\output.jpg", @"C:\Users\proxytype\Desktop\JpgRaperTest\payload.rar", true);

writer.writeJpg(@"C:\Users\proxytype\Desktop\JpgRaperTest\Images\2.jpg", @"C:\Users\proxytype\Desktop\JpgRaperTest\archives\2.zip", @"C:\Users\proxytype\Desktop\JpgRaperTest\output2.jpg", true);

reader.readJpg(@"C:\Users\proxytype\Desktop\JpgRaperTest\output2.jpg", @"C:\Users\proxytype\Desktop\JpgRaperTest\payload2.zip", true);
```
