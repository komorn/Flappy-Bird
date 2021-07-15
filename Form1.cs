using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBirdProgram
{
    /*	
     FLAPPY BIRD
    Natália Komorníková, 1. ročník, Bioinformatika
    Programovaní II zápočtový program
    */
    public partial class Form1 : Form
    {
        enum Stav { START, HRA, KONIEC}    //stavy v ktorých sa hra môže nachádzať, aby som vedela menit, ktor=e komponenty maju byt zobrazene na obrazovke
        Stav stav;

        int rychlost;               //počiatočná rýchlosť pohybu objektov na obrazovke
        int pohybHoreDole = 5;          // zmena polohy vtáčika na obrazovke (posun hore dole)
        double score = 0;               //pocitanie skore, typ double aby som za jednu trubku vedela pripocitat len 0.5 a nie 1, 
                                        //lebo 1 pripočítavam keď sa vtáčik dostane pomedzi obe trubky
        bool gameOver = true;           // boolovska premenna, ktorú používam, aby som vedela spustit hru znova - restart 
                                        //(stlacenie R na klavesnici po prehre) 
        bool prehraBola = false;        //boolovska premenna aby som vedela rozlíšiť či ide o úplný štart(klávesa S) hry, alebo reštart(klávesa R)
        int i;                          //premenna k meneniu rychlosti pohybu objektov v priebehu hry
        private static Random random = new Random();      //instancia random pre poprehadzovanie poradia trubiek v hre
        double najvyssieSkore = Int32.MinValue;             //premenná pre pamatanie si najvyššieho dosiahnutého skóre hráča

        public Form1()
        {
            InitializeComponent();
            NastavStav(Stav.START);     
                        //pri spustení nastavíme stav na START - vysvetlene pri funkcii NastavStav
        }

        private void Stlacene(object sender, KeyEventArgs e)
        {   
            /*
             funkcia, ktorá vykonáva príkazy podľa toho aká klávesa je stlačená
             */

            if (e.KeyCode == Keys.Space)    //pri stlačení medzerníka sa vtáčik pohne smerom nahor (budem prirátavať negatívnu hodnotu)
                pohybHoreDole = -5;
            if (e.KeyCode == Keys.S && gameOver == true && prehraBola == false)        //stlačenie klávesy S pre spustenie hry, 
                                                                //premenná gameover je nazačiatku vždy nastavená na hodnotu false
            {
                NastavStav(Stav.HRA);           //stav hry nastvaníme na hru, aby boli zobrazené len potrebné komponenty
                Start();                        //zavolám funkciu, ktorá umiestni všetky objekty hry na ich počiatočné pozície
                                                // a spustí časovač 
            }

            if (e.KeyCode == Keys.R && gameOver == true && prehraBola == true)          //stlačenie klávesy R po prehre pre spustenie novej hry
            {
                NastavStav(Stav.HRA);               //stav hry nastvaníme na hru, aby boli zobrazené len potrebné komponenty
                Start();                             //zavolám funkciu, ktorá umiestni všetky objekty hry na ich počiatočné pozície
                                                     // a spustí časovač 
            }

        }

        private void Nestlacene(object sender, KeyEventArgs e)
        {
            /*funkcia, ktorá vykonáva príkazy ak daná klávesa nie je stlačená*/
            if (e.KeyCode == Keys.Space)    //ak nemám stlačený medzerník. tak k pozícii vtáčika na obrazovke pripočítavam kladnú hodnotu 
                pohybHoreDole = 5;          //čo znamená jeho pohyb nadol - pomyselné padanie

        }
        private void Start()
        {
            /*funkcia pomocou ktorej nastavím začiatok hry - umiestneni komponent na ich miesta, štartovacia pozícia vtáčika, rozloženie prekážok...*/
            timer1.Enabled = true;      //sprístupním časovač - od nikadiaľ inde ho nechcem spúšťať, až pri štarte
            score = 0;                  // počiatočné skóre pri spustení/ reštartovaní hry je vždy 0
            i = 1;                      //premennú i nastavím na 1, aby som vedela dobre a správne meniť rýchlosť pri každej novej hre
                                        // hodnota 1, lebo rýchlosť mením prvýkrát až keď dosiahnem skóre 5
            gameOver = false;           // hodnotu že hráč prehral nastavím na začiatku hry na false
            List<int> polohyTrubiek = new List<int>() { 500, 800, 1100, 1400, 1700, 2000 };  //list s hodnotami poloh jednotlivych trubiek - 
                                                                                            //list, aby som ho vedela vzdy jednoducho               poprehadzovat  pomocou generatora
            Generator(polohyTrubiek);       //prehadzanie listu pri kaźdom starte - aby boli prekazky v kazdej hre umiestnene inak

            FBird.Location = new Point(57, 57);     //počiatočná pozícia vtáčika v okne

            foreach (var trubka in this.Controls.OfType<PictureBox>())       
            {
                //cyklus foreach, v ktorom jednotlivé prekážky umiestnim na ich generátorom dané pozície v hracom poli 
                if ((string)trubka.Tag == "prekazka1")
                {
                    trubka.Left = polohyTrubiek[0];
                }
                if ((string)trubka.Tag == "prekazka2")
                {
                    trubka.Left = polohyTrubiek[1];
                }
                if ((string)trubka.Tag == "prekazka3")
                {
                    trubka.Left = polohyTrubiek[2];
                }
                if ((string)trubka.Tag == "prekazka4")
                {
                    trubka.Left = polohyTrubiek[3];
                }
                if ((string)trubka.Tag == "prekazka5")
                {
                    trubka.Left = polohyTrubiek[4];
                }
                if ((string)trubka.Tag == "prekazka6")
                {
                    trubka.Left = polohyTrubiek[5];
                }
            }
            timer1.Start();             //všetko umiestné na mieste a spúšťam časovač - dostávam sa do funkcie timer1_tick
        }

        private static List<int> Generator(List<int> c)
        {
            //pri zavolaní štartu sa vždy vygeneruje nové náhodné poradie prekážok pomocou prehádzania poradia hodnot v liste 
            int n = c.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);         //vyberie náhodné číslo z 0 až dĺžka listu
                int value = c[k];                   //vymení náhodné s n-tým, n znižujem každým prechodom, teda posledná hodnota sa už nezmení až kým nezmením všetky za prvou hodnotou
                c[k] = c[n];                        
                c[n] = value;
            }
            return c;               //vráti poprehadzovaný list s polohami trubiek(poloha trubky = ľavá súradnica jednotlivých prekážok)
        }
        private void MenicRychlosti(double sc)
        {
            /*funkcia, pomocou ktorej mením rýchlosť pohybu objektov na obrazovke, čo spôsobí efekt toho, že vtáčik sa hýbe rýchlejšie*/
            if (sc > 5 * i)      //ak je skóre násobok 5 
            {
                rychlost = rychlost + 2;   //rýchlosť zvýšim o 2
                i++;                        // hodnotu i zvýšim o 1 aby som zvýšila rýchlosť znova pri ďalšom násobku 5
            }
        }
        private void Koniec()
        {
            /*funkcia k ukončeniu hry*/
            if (score > najvyssieSkore)         //pamatám si hráčovo najvyššie dosiahnuté skóre, 
                najvyssieSkore = score;               //ktoré vypíšem keď ukončí hru, aby vedel aké najvyššie skóre zatiaľ dosiahol

            timer1.Stop();                  //zastavím časovač, objekty sa prestanú hýbať
            gameOver = true;                //premennú gameOver nastavím na true, lebo hráč prehral a viem týmto podmieniť možnosť reštaru hry
            LSkore.Text += " !!Prehrali ste!!";    //Prehra
            LVysledok.Text = "Vaše skóre: " + score + "\n Najvysššie dosiahnuté skóre: " + najvyssieSkore + "\n Stlačte R pre novú hru.";
                                    //vytvorím label, v ktorom sa hráč dozvie aké skóre dosiahol v tejto hre, aké najvyššie skóre zatiaľ dosiahol 
                                    // a ako spustí novú hru
            prehraBola = true;      //premennú prehraBola nastavím na true, aby som vedela rozlíšiť S a R ako štart a reštart hry
        }
        private void timer1_tick(object sender, EventArgs e)
        {
            //čo sa deje pri každom tiknutí časovača, interval - 20 milisekúnd
            LSkore.Text = "SKÓRE: " + score;    //zapisovanie(vypísanie v okne hry) skóre
            LRychlost.Text = "Rychlost: " + rychlost;       //v okne vypisujem taktiž súčasnú rýchlosť vtáčika, aby hráč vedel akej rýchlosti dosiahol
            FBird.Top += pohybHoreDole;              //hybanie vtacika hore dole (podľa toho či je/nie je stlačený medzerník)

            Rectangle Naraz = new Rectangle(FBird.Left, FBird.Top, FBird.Width - 2, FBird.Height - 3);     //oblast nárazu vtáčika - vytvorila som modifikovaný štvorec aby to bolo trochu presnejšie ako iba FBird.Bounds, 
                                                                                                            //no aj tak to neni úplne presné
            if (FBird.Top < -20 || Naraz.IntersectsWith(Ground.Bounds))             //kontrola, či vtáčik nevyletel moc vysoko mimo okno hry alebo či nenarazil do zeme
            {
                NastavStav(Stav.KONIEC);                                //ak áno ukončujeme hru, stav nastavím na KONIEC a zavolám funkciu koniec, 
                Koniec();                                               //aby sa zobrazilo všetko potrebné pre hráča ako postupovať ďalej a zobrazilo sa mu jeho dosiahnuté skóre
            }

            foreach (var trubka in this.Controls.OfType<PictureBox>())      //pre každý objekt v okne, ktorý je typu PictureBox 
            {
                if ((string)trubka.Tag == "prekazka1" || (string)trubka.Tag == "prekazka2" || (string)trubka.Tag == "prekazka3" ||
                    (string)trubka.Tag == "prekazka4" || (string)trubka.Tag == "prekazka5" || (string)trubka.Tag == "prekazka6")
                    //zároveň ak je jeho značka prekážkaX (X - číslo od 1 do 6)
                {
                    trubka.Left -= rychlost;           //posúvanie prekážok smerom doľava mimo obrazovku v aktuálne danej rýchlosti
                                                        // na začiatku začiatočná rýchlosť, postupne zvyšujeme podľa skóre pomocou funkcie MenicRychlosti()

                    if (trubka.Left < -100)         // ak prekážka vyjde mimo okna hry presunieme prekážku zasa na opacnu stranu(pravý neviditeľný koniec)
                    {
                        trubka.Left = 1700;
                        score += .5;                    //a za každú jednu trubku pripočítam skóre 0.5, lebo mám dve vždy dve trubky, 
                                                        //ktoré dávajú dokopy skóre +1, ak obe vyjdu mimo obrazovku 
                                                        //(za každý objekt typu PictureBox so značkou prekazka X pripočítam 0.50)
                    }

                    Rectangle Trubka = new Rectangle(trubka.Left, trubka.Top, trubka.Width, trubka.Height);      //oblast trubky, do ktorej nemôže vtáčik naraziť, inak prehra

                    if (Naraz.IntersectsWith(Trubka))    //ak vtáčik narazí do trubky nastavím stav KONIEC a zavolám funkciu koniec (ako pri narazení do zeme/ vyletení moc vysoko)
                    {
                        NastavStav(Stav.KONIEC);
                        Koniec();
                    }
                }
            }
            MenicRychlosti(score);                  //tu volám funkciu ktorá mení rýchlost pohybu podľa aktuálneho skóre
        }

        void NastavStav(Stav novy)
        {
            /*funkcia, pomocou ktorej nastavujem aké komponenty majú byť kedy v priebehu hry vidieť a kedy nemajú byť vidieť, 
             prípadne čo má byť v jednotlivých labels napísané*/
            switch (novy)
            {
                case Stav.START:
                    LSkore.Visible = false;             //pri spustení hry skóre nevidno, lebo ešte sme nezačali XD
                    LSkore.Text = "SKORE: " + score;      
                    LNavod.Text = "Vitajte v hre FLAPPY BIRD"+              //pri spustení sa v okne hry zobrazí návod
                        "\n Cieľom hry je nenaraziť do žiadnej prekážky"+
                        "\n a dosiahnuť čo najvyššie skóre!"+
                        "\n Pre spustenie hry stlačte S";
                    LNavod.Visible = true;     // a ten je vidieteľný len v tomto stave inde nie
                    LVysledok.Visible = false;          //vysledok nevidno - až v stave koniec
                    LRychlost.Visible = false;
                    break;
                case Stav.HRA:
                    if (stav == Stav.START || (stav == Stav.KONIEC))
                        rychlost = 5;                                   //ak sme došli do stavu HRA zo stavu START alebo KONIEC nastavíme rýchlosť na počiatočnú - 5
                    LNavod.Visible = false;
                    LSkore.Visible = true;
                    LVysledok.Visible = false;
                    LRychlost.Visible = true;
                    break;
                case Stav.KONIEC:
                    LNavod.Visible = false;
                    LSkore.Visible = true;
                    LVysledok.Visible = true;
                    LRychlost.Visible = false;
                    break;
                default:
                    break;
            }
            stav = novy;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
