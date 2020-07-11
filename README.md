# JpgRaper
Hide compressed files inside jpg, most jpg encoders looks only on the image stream and ignore any extra infromation,  
because this "feature" we can inject files into jpg file and keep the behavior as image in most systems.

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
