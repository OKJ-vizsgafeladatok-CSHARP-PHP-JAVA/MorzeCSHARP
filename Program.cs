using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MorzeGYAK
{
    class Program
    {
        public static List<MorzeABC> morzeABC = beolvasABC();
        public static List<MorzeSzoveg> morzeSzoveg = beolvasSzoveg();

        public static List<MorzeABC> beolvasABC()
        {
            List<MorzeABC> lista = new List<MorzeABC>();
            try
            {
                FileStream fs = new FileStream("morzeabc.txt",FileMode.Open);
                using (StreamReader sr=new StreamReader(fs,Encoding.UTF8))
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        var split = sr.ReadLine().Split('\t');
                        var o = new MorzeABC(split[0],split[1]);
                        lista.Add(o);
                    }
                }
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba a beolvasásnál. ABC "+e);
            }
            return lista;
        }

        public static List<MorzeSzoveg> beolvasSzoveg()
        {
            List<MorzeSzoveg> lista = new List<MorzeSzoveg>();
            try
            {
                FileStream fs = new FileStream("morze.txt",FileMode.Open);
                using (StreamReader sr=new StreamReader(fs,Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        var split = sr.ReadLine().Split(';');
                        var o = new MorzeSzoveg(split[0],split[1]);
                        lista.Add(o);
                    }
                }
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba a beolvasásnál. szoveg "+e);
            }
            return lista;
        }

        public static string Morze2Szöveg(string kod, List<MorzeABC>abc)
        {
            string kodolatlan="";
            var szavak = kod.Replace("       ","@").Split('@');
            for (int i = 0; i < szavak.Length; i++)
            {
                var szo = szavak[i].Replace("   ","#").Split('#');
                for (int j = 0; j < szo.Length; j++)
                {
                    foreach (var item in abc)
                    {
                        if (item.kod.Equals(szo[j]))
                        {
                            kodolatlan += item.betu;
                        }
                    }
                }
                kodolatlan += " ";
            }

            return kodolatlan;
        }

        static void Main(string[] args)
        {
            #region 3. feladat
            Console.WriteLine($"A morze abc {morzeABC.Count} db karakter kódját tartalmazza. ");
            #endregion

            #region 4. feladat
                string karaktertkeres(string karakter)
            {
                var eredmeny="\tNem található a kódtárban ilyen karakter! ";
                karakter = karakter.ToUpper();
                var talalat = morzeABC.SingleOrDefault(x=>x.betu==karakter);
                if (talalat!=null)
                {
                    eredmeny = "\tA "+karakter+" karakter morze kódja: "+talalat.kod;
                }
                return eredmeny;
            }

            Console.Write("4. feladat: Kérek egy karaktert: ");
            string beker=Console.ReadLine();
            Console.WriteLine(karaktertkeres(beker));
            #endregion

            #region 7. feladat
            Console.WriteLine("7. feladat: Az első idézet szerzője: "+Morze2Szöveg(morzeSzoveg.First().szerzo,morzeABC));
            #endregion

            #region 8. feladat
            var max = 0;
            MorzeSzoveg maxAlany = null;
            foreach (var item in morzeSzoveg)
            {
                if (Morze2Szöveg(item.idezet, morzeABC).Length>max) 
                {
                    maxAlany=item;
                    max = Morze2Szöveg(item.idezet, morzeABC).Length;
                }
            }

            Console.WriteLine("8. feladat: A leghosszabb idézet szerzője és az idézet: "+
                Morze2Szöveg(maxAlany.szerzo,morzeABC)+
                ": "+
                Morze2Szöveg(maxAlany.idezet,morzeABC)
                );
            #endregion

            #region 9. feladat
            Console.WriteLine("9. feladat: Arisztotelész idézetei:");
            foreach (var item in morzeSzoveg)
            {
                if (item.szerzo==morzeSzoveg.First().szerzo)
                {
                    Console.WriteLine("\t- "+Morze2Szöveg(item.idezet,morzeABC));
                }
            }
            #endregion

            #region 10. feladat:
            using (StreamWriter sw=new StreamWriter(new FileStream("forditas.txt",FileMode.Create),Encoding.UTF8))
            {
                morzeSzoveg.ForEach(
                    x=>
                        {
                            sw.WriteLine(Morze2Szöveg(x.szerzo,morzeABC).Remove(Morze2Szöveg(x.szerzo, morzeABC).Length-1) +":"+Morze2Szöveg(x.idezet,morzeABC));                    
                        }
                    );
            }
            #endregion

                Console.ReadKey();
        }
    }
}
