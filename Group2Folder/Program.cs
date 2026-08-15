using System.Text.RegularExpressions;

Console.WriteLine("This script helps to group files to folder by filename template");
Console.WriteLine("IMG_YYYYMMDD_******.jpg -> /YYYY/YYYY-MM/IMG_YYYYMMDD_******.jpg");

string pattern = @"^(IMG|PANO|VID)[-_](\d{4})(\d{2})(\d{2})[-_](.{2}).*\.(jpg|mp4)$";
string sourcePath = Path.Join(@"D:\photo\2019");
string subfolder;

int count = 0;
int limit = 5000;

foreach(var file in Directory.GetFiles(sourcePath)){
    string filename = Path.GetFileName(file);
    var match = Regex.Match(filename, pattern);
    subfolder = ( match.Success )
        ?Path.Join(sourcePath,
        match.Groups[2].Value, 
        match.Groups[1].Value, 
        (match.Groups[5].Value=="WA")?"WhatsApp":"",  
        string.Join("_", 
            match.Groups[2].Value, 
            match.Groups[3].Value, 
            match.Groups[4].Value) )
        :Path.Join(sourcePath, "ungroupped" );
    
    if (!Directory.Exists(subfolder))
        Directory.CreateDirectory(subfolder);
    File.Copy(file, Path.Combine(subfolder, filename));

    Console.WriteLine(++count +"\t"+ filename+"\tcopied to "+subfolder); 
    if (count>limit) break;
}