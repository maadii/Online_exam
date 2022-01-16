using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Tools
{
     public class NationalCodeValidation
    {
        public bool IsValid(string codes)
        {

            try
            {
                if (codes.Length == 10)
                {
                    int[] a = new int[10];
                    int m = Convert.ToInt32(codes);
                    while (m != 0)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            a[i] = m % 10;
                            m /= 10;
                        }
                    }
                    int AllMu = 0;
                    for (int i = 1; i < a.Length; i++)
                    {
                        AllMu += a[i] * (i + 1);
                    }
                    int R = AllMu % 11;
                    if (R <= 2)
                    {
                        if (a[0] == R)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (a[0] == 11 - R)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
