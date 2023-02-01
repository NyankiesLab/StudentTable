using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Entity
{
    abstract class IOgrenci
    {
        public abstract int ortalamaHesapla();
        public abstract int sinavKontrolEt(int target);
    }
}
