using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LoremTurk
{
    public partial class Form1 : Form
    {
        string[] array;

        //string sessizler = "bcçdfghjklmnpqrsştvwyzxBCÇDFGHJKLMNPQRSTUVWXYZ";
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //array = File.ReadAllLines("turkce.dbt");
        }

        private string kelimesec(int minlen)
        {
            
            int index = rnd.Next(1, array.Length-20);
            reroll:
            string[] parts = array[index].Split(' ');
            string kelime = parts[0];
            if (kelime.Length < minlen)
            {
                index++; //ilerledikçe kelime uzunluğu artıyor
                goto reroll;
            }
            return kelime;
        }

        private string sonkelimesec(int minlen)
        {

            int index = rnd.Next(1, array.Length - 20);
        reroll:
            string[] parts = array[index].Split(' ');
            string kelime = parts[0];
            if ((kelime.Length < minlen) || (kelime.Substring(kelime.Length-1)!="r"))
            {
                index++; //ilerledikçe kelime uzunluğu artıyor
                if (index > array.Length - 1) index = 1;

                goto reroll;
            }
            return kelime;
        }

        private string kalinkelimesec(int minlen, bool yuklem)
        {
           
            int index = rnd.Next(1, array.Length - 20);
        reroll:
            string[] parts = array[index].Split(' ');
            string kelime = parts[0];
            if ((kelime.Length < minlen) || (yuklem && (kelime.Substring(kelime.Length - 1) != "r")))
            {
                index++; //ilerledikçe kelime uzunluğu artıyor
                if (index > array.Length - 1) index = 1;

                goto reroll;
            }
            if (unlutipi(kelime) != 2)
            {
                index=index+100; //uzaktaki bir kelimeye sicra
                if (index > array.Length - 1) index = rnd.Next(100,500);

                goto reroll;
            }
            return kelime;
        }

        private string incekelimesec(int minlen,bool yuklem)
        {

            int index = rnd.Next(1, array.Length - 20);
        reroll:
            string[] parts = array[index].Split(' ');
            string kelime = parts[0];
            if ((kelime.Length < minlen)|| (yuklem && (kelime.Substring(kelime.Length - 1) != "r")))
            {
                index++; //ilerledikçe kelime uzunluğu artıyor
                if (index > array.Length - 1) index = 1;

                goto reroll;
            }
            if (unlutipi(kelime)!=1)
            {
                index = index + 100; //uzaktaki bir kelimeye sicra
                if (index > array.Length - 1) index = rnd.Next(100, 500);

                goto reroll;
            }
            return kelime;
        }

        private bool incemi(string z)
        {
            string sesliler = "eiöü";
            for (int x = 0; x < sesliler.Length; x++)
            {
                if (z == sesliler.Substring(x, 1)) { return true; }

            }
            return false;
        }

        private int unlutipi(string z)
        {
            string s = z;
            int tip = 0;
            //Tip 0: sesli yok, 1:ince, 2: kalin, 3:karisik
            string isesliler = "eiöü";
            string ksesliler = "aouı";
            int ince = 0;
            int kalin = 0;
       

    
                var charsToRemove = new string[] { "b", "c", "d", "f", "ç", "g", "ğ", "h", "j", "k", "l", "m", "n", "p", "r", "q", "s", "ş", "t", "v", "w", "x", "y", "z" };
                foreach (var c in charsToRemove)
                {
                    z = z.Replace(c, string.Empty);
                }
                ince = 0;
                for (int y = 0; y < z.Length; y++)
                {
                    for (int x = 0; x < isesliler.Length; x++)
                    {
                        if (z.Substring(y,1) == isesliler.Substring(x, 1)) { ince = 1; break; }

                    }
                    if (ince == 1) break;
                }
                kalin = 0;
                for (int y = 0; y < z.Length; y++)
                {
                    for (int x = 0; x < ksesliler.Length; x++)
                    {
                        if (z.Substring(y , 1) == ksesliler.Substring(x, 1)) { kalin = 2; break; }

                    }
                    if (kalin == 2) break;
                }
            tip = ince + kalin;

            return tip;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) textBox1.Text = rnd.Next(3, 20).ToString();

            int say = Convert.ToInt32(textBox1.Text);
            for (int q = 1; q < say; q++) //cümle sayısı
            {
                int son = rnd.Next(3, 9);
                for (int x = 1; x <= son; x++) //kelime sayısı
                {
                    string yeni = "";
                    int kelimeturu = rnd.Next(0, 4); //0:kuralsiz, 1: buyuk unlu uyumu


                    string[] hecelerx=new string[30];
                    
                    int sestipi=3;
                    int cson = rnd.Next(3, 7);

                    for (int c = 0; c < cson; c++) //hece sayısı
                    {
                        int maxlen = c * 3;
                        if (maxlen > 21) maxlen = 21;

                        string kelime;
                        if (x == son)
                        {
                            
                            if (kelimeturu != 0)
                            {
                                if (sestipi == 1)
                                {
                                    kelime = incekelimesec(maxlen, true);
                                }
                                else if (sestipi == 2)
                                {
                                    kelime = kalinkelimesec(maxlen, true);
                                }
                                else
                                {
                                    kelime = sonkelimesec(maxlen);
                                }
                            }
                            else
                            {
                                kelime = sonkelimesec(maxlen);
                            }

                        }
                        else
                        {

                            if (c == 0)
                            {
                                //ilk kelimeyi rastgele seçelim
                                kelime = kelimesec(maxlen);
                            }
                            else
                            {
                                if (kelimeturu != 0)
                                {
                                    if (sestipi == 1)
                                    {
                                        kelime = incekelimesec(maxlen,false);
                                    }
                                    else if (sestipi == 2)
                                    {
                                        kelime = kalinkelimesec(maxlen,false);
                                    }
                                    else
                                    {
                                        kelime = kelimesec(maxlen);
                                    }
                                }
                                else
                                {
                                    kelime = kelimesec(maxlen);
                                }

                            }
                        }


                        //txt.Text = txt.Text + c.ToString()+ kelime + ":";
                        //bir kelime aldık, bunu hecelerine ayıralım
                        string[] heceler = kekele(kelime);
                        hecelerx = heceler;
                        int hc;
                        if ((cson > 6) ||(x==son))
                        {
                            hc = (heceler.Length -1) - (cson - c);
                            
                           
                        }
                        else
                        {
                            hc = c + (rnd.Next(0, 1));
                            
                        }
                        if ((hc > heceler.Length - 1) || (hc<0)) hc = heceler.Length - 1;
                        if (heceler[hc] == "") heceler[hc] = "dar";
                        if ((x == son) && (c == cson-1))
                        {
                            if (rnd.Next(1, 10) > 7)
                            {
                                //%30 ihtimalle virgül, soru işareti
                                string virgul = ",,,,,,,,;!!:??.";
                                heceler[hc] = virgul.Substring(rnd.Next(0, virgul.Length - 2), 1);
                            }
                            else
                            {
                                heceler[hc] += ".";
                            }
                        }
                        yeni = yeni + heceler[hc];
                        if (c == 0)
                        {
                            //ilk hece, ünlü tipini bul
                            sestipi = unlutipi(heceler[c]);
                            
                        }


                    }
                    //txt.Text = txt.Text + ".\r\n";
                    //cumle.Text = cumle.Text + yeni + " ";
                    if (yeni.Substring(0, 1) == "ğ") yeni = 'g' + yeni.Substring(1); 

                    if (x == 1)
                    {
                        //cümle başı
                        
                        yeni = yeni.Substring(0,1).ToUpper() + yeni.Substring(1);                       
                    }
                    if (x == son)
                    {
                        yeni = yeni + hecelerx[hecelerx.Length - 1];
                    }
                    txt.Text = txt.Text +  " "+yeni;

                }
                //txt.Text = txt.Text + ".";
            }
            txt.Text = txt.Text + "\r\n \r\n";
        }


        private bool seslimi(string z)
        {
            //string sesliler = "AaEeOoUuIıÜüİiÖö";
            string sesliler = "aeouıüiö";
            for (int x = 0; x < sesliler.Length; x++)
            {
                if (z == sesliler.Substring(x, 1)) { return true; }

            }
            return false;
        }



        private string[] kekele(string kelime)
        {
            string cikti="";


                    string[] hece = new string[101];
                    int heceler =  100;
                    int buluntu = -1;

                //adim0:
                    if (kelime.Length == 1)
                    {
                           hece[100] = kelime ;
                           heceler = 99;
                           goto kelimebitti;
                    }

                adim1:
                    if (kelime.Length < 2)
                    {
                        //kelime bitmiş ya da çok kısa.

                        if (seslimi(kelime))
                        {
                            //sesli harf hecesi
                            hece[heceler]=kelime;
                            heceler = heceler - 1;
                        }
                        else
                        {
                            //sessiz harften hece olmaz sonraki heceye ekleyelim.
                            hece[heceler+1]=kelime+hece[heceler+1];

                        }
                        goto kelimebitti;
                    }
           
                

                    buluntu = -1;
                    for (int y = kelime.Length; y > 1; y--)
                    {
                        string harf = kelime.Substring(y-1, 1);
                        if (seslimi(harf))
                        {
                            buluntu = y-1;
                            break;
                        }

                    }

                    if (buluntu == -1)
                    {
                        //sesli harf yok-sayı, noktalama vb
                        hece[heceler] = kelime;
                        goto hecebitti;
                    }

                    if (buluntu == 0)
                    {
                        //bu ilk harf, ozaman diğerleri sessiz demektir, biz toplayıp bitirelim

                        hece[heceler] = kelime.Substring(buluntu);
                        goto hecebitti;
                    }


                    if (seslimi(kelime.Substring( buluntu - 1, 1))==false)
                    {
                        hece[heceler]=kelime.Substring(buluntu-1);
                    }
                    else
                    {
                        hece[heceler]=kelime.Substring(buluntu);
                    }
                 


                hecebitti:
                    //hecebitti, kelimeden bulduğumuz en son heceyi düşüyoruz
                    kelime=kelime.Substring(0,(kelime.Length-hece[heceler].Length));
                    heceler = heceler - 1;
                    goto adim1;


                kelimebitti:
                    string[] hecex=new string[101-heceler];
                int ch = 0;
                    for (int f = heceler + 1; f < 101; f++)
                    {
                        hecex[ch] = hece[f];
                        ch++;
                        cikti = cikti + hece[f] + "-";
                    }
                //txt.Text = txt.Text+" ... "+cikti+"\r\n";
                return hecex;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                int kar = 0;
                int ince = 0;
                int kalin = 0;
                int[] uzu=new int[50];
            for (int index = 0; index < array.Length; index++)
            {
                string[] parts = array[index].Split(' ');
                string kelime = parts[0].Trim();
                int tip=unlutipi(kelime);
                if ((tip==3)||(tip == 0)) kar++;
                if (tip == 1) ince++;
                if (tip == 2) kalin++;
                uzu[kelime.Length]++;

            }

            txt.Clear();
            txt.Text = "ince:" + ince.ToString() + " Kalin:" + kalin.ToString() + " mix:" + kar.ToString() + "\r\n";
            txt.Text += "uzunluklar:";
            for (int x = 0; x < 50; x++)
            {
                txt.Text += x.ToString() + ": " + uzu[x].ToString()+"\r\n";

            }




        }

        private void cumle_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int kc = 0;
            string[] words = new string[20000];
            int[] fr = new int[35];
            string harflist = "abcçdefgğhıijklmnoöpqrsştuüvwxyz";

            for (int index = 0; index < array.Length; index++)
            {
                string[] parts = array[index].Split(' ');
                string kelime = parts[0].Trim();
                int l = kelime.Length;
                if (l == 5)
                {
                    kc++;
                    words[kc] = kelime;

                    for (int k = 0; k < kelime.Length; k++)
                    {
                        for (int x = 0; x < harflist.Length; x++)
                        {
                            if (kelime.Substring(k, 1) == harflist.Substring(x, 1))
                            {
                                fr[x]++;
                            }

                        }

                    }


                }



            }
            txt.Clear();
            int t = 0;
            foreach (int r in fr)
            {
                t++;

                if (r > 0) txt.Text += harflist.Substring(t - 1, 1) + ": " + r.ToString() + "\r\n";

            }

            txt.Text += "\r\n **** 3 kelime arama ***\r\n";

            array = File.ReadAllLines("5li.dbt");
            //string harflist = "abcçdefgğhıijklmnoöpqrsştuüvwxyz";

            int[] skiplist = new int[1000]; //burda tekrar tekrar kontrol edilecek indeksleri eliyoruz.
            int skipid = 0;
            int[] foundlist = new int[4]; 
            int bulunankelime=0;
            bool bulunmadi = true;
            while (bulunmadi)
            {
                if (bulunankelime==3 )break; // 3 tane bulduk!!!

                string done = "";
                for (int index = 0; index < array.Length; index++)
                {
                    bool skip=false;
                    for (int i=0;i<1000;i++)
                    { 
                        if (skiplist[i]==index)
                        {
                            skip=true;
                        }
                    }
                    if (!skip)
                    {
                        string[] parts = array[index].Split(' ');
                        string kelime = parts[0].Trim();
                        string olddone = done;
                        bool notfound = false;
                        for (int k = 0; k < kelime.Length; k++)
                        {
                            if (!varmi(done, kelime.Substring(k, 1)))
                            {
                                //bu harf yok
                                for (int x = 0; x < harflist.Length; x++)
                                {
                                    if (kelime.Substring(k, 1) == harflist.Substring(x, 1))
                                    {
                                        done = done + kelime.Substring(k, 1);

                                    }

                                }
                            }
                            else
                            {
                                //bu harf kullanılmış, bu kelime işe yaramaz, silelim ve devam edelim
                                done = olddone;
                                notfound = true;
                                break;
                            }


                        }
                        //kelime harfleri bitti
                        if (!notfound)
                        {
                            //bu kelime kabul!
                            foundlist[bulunankelime] = index;
                            bulunankelime++;
                            txt.Text += (bulunankelime+1).ToString() +" "+ kelime+"\r\n";
                            if (bulunankelime > 2)
                            {
                                bulunmadi = false;
                            }

                        }
                        else
                        {
                            //bu kelime red
                        }

                    }//end skip
                }//end of array
                if (bulunankelime < 3)
                {
                    //malesef yeterli kelime bulamadık
                    //başa sarıcaz ama ilk kelimeyi işaretliycez.
                    skiplist[skipid] = foundlist[0];
                    skipid++;
                    txt.Text += "Skip..." + skipid+"\r\n";
                    bulunankelime = 0;

                }
            } //end while
            txt.Text += "\r\n BİTTİ";

        }

            private bool varmi(string harflistesi,string harf)
            {
          
                        for (int x = 0; x < harflistesi.Length; x++)
                        {
                            if ( harf == harflistesi.Substring(x, 1))
                            {
                                return true;
                            }
                        }
                return false;
            }
        private void button4_Click(object sender, EventArgs e)
        {
            array = File.ReadAllLines("turkce.dbt");
            button4.BackColor = Color.GreenYellow;
            button5.BackColor = Color.LightGray;
            button1.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            array = File.ReadAllLines("tdk.dbt");
            button5.BackColor = Color.GreenYellow;
            button4.BackColor = Color.LightGray;
            button1.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txt.Clear();

        }

    }
}
