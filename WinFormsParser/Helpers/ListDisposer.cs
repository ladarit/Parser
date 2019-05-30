using System;
using System.Collections.Generic;
using System.Linq;
using GovernmentParse.Models;

namespace GovernmentParse.Helpers
{
    public static class ListDisposer
    {
        public static void Clear(this FileModel file)
        {
            Array.Clear(file.Content, 0, file.Content.Length);
            file.Content = null;
        }

        public static void ClearCollection(this List<FileModel> collectionToClean, FilesToSave comparedCollection)
        {
            foreach (var file in collectionToClean)
                if (comparedCollection.Files.FirstOrDefault(f => f.FileName.Contains(file.FileName)) == null)
                    file.Dispose();
        }

        public static void ClearCollection(this List<FileModel> collectionToClean)
        {
            foreach (var file in collectionToClean)
                    file.Dispose();
        }
    }
}
