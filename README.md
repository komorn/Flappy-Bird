Autor: Natália Komorníková

Ako zápočtový program tento semester som si vybrala hru Flappy Bird.
Cieľom hry Flappy Bird je dostať sa lietajúcim vtáčikom čo najďalej, preletením pomedzi prekážky. Nabúraním do prekážok, vyletením príliš vysoko alebo narazením do zeme hráč hru automaticky prehráva. nenabúrať do nich a zároveň nevyletieť príliš vysoko, alebo nespadnúť na zem. Kliknutím/držaním medzerníka sa vtáčik pohybuje smerom nahor a ak medzerník nie je stlačený vtáčik padá smerom nadol. 

Program som vytvárala tak, že som si najprv dala všetky obrázky do okna hry. Vtáčika som umiestnila na jeho počiatočnú pozíciu a určila som si ako budem vytvárať ilúziu toho, že vtáčik prelietava ďalej pomedzi prekážky – posúvaním prekážok po obrazovke v danej rýchlosti. K tomu potrebujem mať integerovú premennú rychlost. Počiatočná hodnota tejto premennej je 5 a v čase sa mení.  Premenná Rychlost mení ľavú súradnicu objektov (prekážok) o svoju hodnotu a tým sa objekty pomyslene posúvajú proti vtáčikovi. Rychlost v priebehu hry mením na základe skóre, ktorého hráč dosiahol vo funkcií MenicRychlosti. Objekty sa teda po obrazovke posúvajú čím ďalej tým rýchlejšie. (vďaka zvyšovaniu rýchlost)

Hra sa môže nachádzať v troch stavoch a to: 

    1.	START – pri spustení, zobrazí návod ako hru Falppy Bird hrať

    2.	HRA – samotné hranie hry, kde sa už objekty posúvajú po obrazovke a počíta sa dosiahnuté skóre

    3.	KONIEC – po narazení vtáčika do prekážky alebo pri vyletení mimo hracej plochy. Zobrazí sa hráčovo najvyššie dosiahnuté skóre od spustenia hry a skóre, ktoré dosiahol v tejto hre. Po stlačení klávesy R hráč spustí novú hru.

Tieto tri stavy rozlišujem, aby som vedela určiť čo má byť kedy zobrazené na obrazovke. Napríklad pri stave START sa musí zobraziť návod, pri stave KONIEC výsledok atď.

K rozlíšeniu, kedy môžem hru spustiť znova a kedy nie používam boolean premennú gameOver – ak je true môžem na klávesnici stlačiť S (pri úplnom spustení hry) alebo R (pri reštarte) a hra sa spustí znova. Prichádza teda k rozlišovaniu spustenia hry po prvý raz a spustenia hry už po predchádzajúcej prehre. K rozlišovaniu prichádza v boolean premennej prehraBola. 

Program funguje tak, že mám natvrdo daných 6 rôznych prekážok, ktoré vždy pred novou hrou náhodne poprehadzujem. Tieto prekážky následne umiestnim na ich počiatočné pozície v okne. Keď hru spustím, všetky prekážky sa začnú posúvať rovnakou rýchlosťou smerom ku vtáčikovi. Akonáhle prekážka zmizne z obrazovky, zaradí sa zasa za poslednú úplne napravo. Takto presúvam jednotlivé prekážky z jednej strany hracej plochy na druhú.

(Tu som zo začiatku mala len 3 prekážky, ale tie sa neustále opakovali v rovnakom poradí čo bolo príliš jednoduché hrať. Preto som si zvolila počet prekážok 6 a zároveň to, že v každej hre sú poukladané za sebou v inom poradí. Chcelo by to niečo ešte menej triviálne, no prekážky nemôžem umiestniť len tak náhodne, lebo ich rozlišujem podľa Tagu. Každá dvojica prekážok má svoj Tag a podľa neho ich pri štarte rozmiestnim.)

Hra funguje vo veľkosti okna, ktorú som si na začiatku zvolila ako podľa mňa najideálnejšiu. Hráč vidí v jednom čase max. tri prekážky. Ak by ich videl viac bolo by jednoduchšie dosiahnuť vyššie skóre.

List polohyTrubiek je náhodne vygenerovaný funkciou Generator() poprehadzovaním prekážok pred každou hrou nanovo. Týmto docielim, aby sa hry (poradie prekážok) neopakovali. Premennú polohyTrubiek som zvolila typu list z dôvodu jednoduchosti prehadzovania poradia jednotlivých elementov pomocou tretej premennej.

//obrázky k fungovaniu hry sa nachádzajú v súbore Resources - bird (vtáčik), ground (zem), pipe (spodná trubka), pipedown (horná trubka)

Hlavný súbor, ktorý treba spustiť pre exekúciu hry - Form1.cs
