using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Veterinaria.Controls
{
    public static class ViewExtensions
    {
        public static Page? FindParentPage(this Element element)
        {
            while (element != null)
            {
                if (element is Page page)
                    return page;

                element = element.Parent;
            }
            return null;
        }
    }
}
