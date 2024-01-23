namespace Helpers;

public static class ArrayHelper
{
    
    public static void IterateTrough2DArray(Action<int,int> func,int first,int second)
    {
        for (var i = 0; i < first; i++)
        {
            for (var j = 0; j < second; j++)
            {
                func(i,j);
            }
        }
    }
    public static void IterateTrough2DArray<T>(Action<int,int,T> func,int first,int second,T arg)
         {
             for (var i = 0; i < first; i++)
             {
                 for (var j = 0; j < second; j++)
                 {
                     func(i,j,arg);
                 }
             }
         }
    public static void IterateTrough2DArray<T,TK>(Action<int,int,T,TK> func,int first,int second,T arg1,TK arg2)
    {
        for (var i = 0; i < first; i++)
        {
            for (var j = 0; j < second; j++)
            {
                func(i,j,arg1,arg2);
            }
        }
    }
    
}