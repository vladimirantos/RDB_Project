Základní operace s linqem:
http://www.c-sharpcorner.com/UploadFile/3d39b4/simple-select-insert-update-and-delete-using-linq-to-sql/

KniHOVNA
http://filehelpers.sourceforge.net/

Paralelni sracky
http://stackoverflow.com/questions/11881929/reading-a-csv-file-with-a-million-rows-in-parallel-in-c-sharp

Cteni od
filestream.seek
https://social.msdn.microsoft.com/Forums/en-US/70784fe4-2b89-4d58-ae05-38bff94c3006/how-to-read-big-text-files-in-c-threads?forum=csharplanguage


const Int32 BufferSize = 128;
using (var fileStream = File.OpenRead(fileName))
  using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize)) {
    String line;
    while ((line = streamReader.ReadLine()) != null) 
      // Process line
  }

string readContents;
using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
{
     readContents = streamReader.ReadToEnd();
}

http://stackoverflow.com/questions/7387085/how-to-read-an-entire-file-to-a-string-using-c-sharp
http://www.codeproject.com/Articles/9258/A-Fast-CSV-Reader


PARSOVÁNÍ SOUBORU - PLINQ
http://www.google.cz/url?sa=t&rct=j&q=&esrc=s&source=web&cd=9&ved=0CEgQFjAI&url=http%3A%2F%2Fphysics.ujep.cz%2F~jkrejci%2Fvyuka%2Fprg2%2FParallelC.PDF&ei=61ctVYv4IsetygPgyoGQDQ&usg=AFQjCNEAG78qfhdweSgVK_RoQnSFfQisdg&bvm=bv.90790515,d.bGQ


http://blog.netcorex.cz/c-sharp/task-parallel-library-tpl-predstaveni/


Cteni souboru 
http://stackoverflow.com/questions/11881929/reading-a-csv-file-with-a-million-rows-in-parallel-in-c-sharp
