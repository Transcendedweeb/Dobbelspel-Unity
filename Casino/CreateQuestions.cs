using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Subject = CasinoData.Subject;
using Level = CasinoData.Level;

public class CreateQuestions : MonoBehaviour
{
    CasinoData casinoData;

    void SetMuziek1()
    {
        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "Welk muziekinstrument wordt bespeeld met behulp van hamers?", 
            new List<string> { "Drum", "Viool", "Piano", "Trompet" }, 
            2
        );
        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "Welke muziekstijl is ontstaan in de Verenigde Staten en heeft invloeden van Afrikaanse en Europese muziektradities?", 
            new List<string> { "Klassiek", "Jazz", "Rock", "Reggae" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "Wie was de leadzanger van de legendarische rockband Queen?", 
            new List<string> { "Freddie Mercury", "Mick Jagger", "David Bowie", "Bono" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "", 
            new List<string> { "The Rolling Stones", "The Beatles", "Queen", "Led Zeppelin" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "Welke klassieke componist schreef de Vijfde Symfonie, bekend om de vier iconische beginnoten?", 
            new List<string> { "Wolfgang Amadeus Mozart", "Johann Sebastian Bach", "Ludwig van Beethoven", "Antonio Vivaldi" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "Uit welk land komt de muziekstijl samba?", 
            new List<string> { "Spanje", "Brazilië", "Mexico", "Argentinië" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "Welke Canadese zangeres werd wereldwijd beroemd met de hit My Heart Will Go On uit de film Titanic?", 
            new List<string> { "Shania Twain", "Celine Dion", "Alanis Morissette", "Avril Lavigne" }, 
            1
        );
        
        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "Welke instrument bespeelde de legendarische jazzmuzikant Louis Armstrong vooral?", 
            new List<string> { "Saxofoon", "Trompet", "Piano", "Drums" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "Wie wordt vaak The Queen of Pop genoemd?", 
            new List<string> { "Whitney Houston", "Madonna", "Mariah Carey", "Beyoncé" }, 
            1
        );
        
        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "In welk land ontstond de muziekstijl reggae?", 
            new List<string> { "USA", "Jamaica", "Trinidad en Tobago", "Haïti" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level1, 
            "Welke beroemde Spaanse zanger staat bekend om zijn hits Bailamos en Hero?", 
            new List<string> { "Ricky Martin", "Enrique Iglesias", "Shakira", "Luis Fonsi" }, 
            1
        );
    }

    void SetMuziek2()
    {
        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level2, 
            "Wat is de naam van het Led Zeppelin-album uit 1971 waarop de iconische nummers Stairway to Heaven en Black Dog staan?", 
            new List<string> { "Led Zeppelin II", "Led Zeppelin III", "Led Zeppelin IV", "Houses of the Holy" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level2, 
            "Wat is de naam van het avant-gardistische stuk van John Cage dat bestaat uit 4 minuten en 33 seconden stilte?", 
            new List<string> { "Silence", "Emptiness", "4'33", "Stillness" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level2, 
            "Welke invloedrijke Braziliaanse muzikant en componist staat bekend als de grondlegger van de bossa nova-beweging met nummers als The Girl from Ipanema?", 
            new List<string> { "João Gilberto", "Caetano Veloso", "Gilberto Gil", "Tom Jobim" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level2, 
            "Wat was de oorspronkelijke artiestennaam van David Bowie, toen hij voor het eerst begon met het maken van muziek?", 
            new List<string> { "Ziggy Stardust", "David Jones", "Major Tom", "Aladdin Sane" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level2, 
            "Het nummer Smells Like Teen Spirit van Nirvana wordt vaak als het anthem van welke muziekstijl genoemd?", 
            new List<string> { "Punkrock", "Metal", "New wave", "Grunge" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level2, 
            "In welke muziekstijl wordt een clave-ritmepatroon vaak gebruikt, een essentieel kenmerk van de Afro-Cubaanse en Latijns-Amerikaanse muziek?", 
            new List<string> { "Flamenco", "Salsa", "Tango", "Reggaeton" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level2, 
            "Welke album van Pink Floyd uit 1973 wordt beschouwd als een van de best verkochte en meest invloedrijke albums aller tijden?", 
            new List<string> { "The Wall", "Dark Side of the Moon", "Wish You Were Here", "Animals" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level2, 
            "Welke Amerikaanse jazzpianist is bekend om zijn complexe ritmes en componeerde de jazzstandaard Take Five?", 
            new List<string> { "Duke Ellington", "Bill Evans", "Dave Brubeck", "Herbie Hancock" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level2, 
            "Wie componeerde het klassieke stuk Boléro, een langzame maar intense opbouw die slechts één thema herhaalt?", 
            new List<string> { "Claude Debussy", "Maurice Ravel", "Igor Stravinsky", "Georges Bizet" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level2, 
            "Welk nummer van The Beatles wordt vaak genoemd vanwege het gebruik van een sitar en oosterse invloeden, dankzij de interesse van George Harrison in Indiase muziek?", 
            new List<string> { "Here Comes the Sun", "Norwegian Wood", "While My Guitar Gently Weeps", "Blackbird" }, 
            1
        );
    }

    void SetMuziek3()
    {
        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level3, 
            "Welke jazzmuzikant ontwikkelde in de jaren 60 het concept van de “harmolodics,” een methode van improvisatie die vrije tonale structuren gebruikt", 
            new List<string> { "Ornette Coleman", "Charles Mingus", "John Coltrane", "Thelonious Monk" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level3, 
            "Welk album van de Japanse componist Ryuichi Sakamoto, wordt beschouwd als een vroege synthese van ambient pop?", 
            new List<string> { "B-2 Unit", "Thousand Knives", "Kokokomonomono", "Secrets of the Beehive" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level3, 
            "Wat was de oorspronkelijke naam van de Engelse punkband The Sex Pistols voordat ze de naam veranderden naar wat ze nu zijn?", 
            new List<string> { "The Strand", "The Stooges", "The Runners", "The Clash" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level3, 
            "In welke compositie van Igor Stravinsky, voor het eerst uitgevoerd in 1913 in Parijs, brak er een beroemde rel uit vanwege de ongewone ritmes en klanken?", 
            new List<string> { "L'Oiseau de feu", "Le Sacre du Printemps", "Petrouchka", "Symphonies of Wind Instruments" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level3, 
            "De experimentele rockband The Velvet Underground stond bekend om haar samenwerking met een kunstenaar als manager en producer. Wie was deze kunstenaar?", 
            new List<string> { "Roy Lichtenstein", "Andy Warhol", "Jackson Pollock", "Jasper Johns" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level3, 
            "Welk nummer werd door The Beatles in slechts 11 uur opgenomen en wordt vaak beschouwd als het begin van hun experimenten met psychedelische muziek?", 
            new List<string> { "Strawberry Fields Forever", "A Day in the Life", "I Am the Walrus", "Tomorrow Never Knows" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level3, 
            "Welke barokcomponist componeerde Musikalisches Opfer (Musical Offering)?", 
            new List<string> { "Johann Sebastian Bach", "Georg Friedrich Händel", "Antonio Vivaldi", "Johann Pachelbel" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level3, 
            "Welk vroege hiphopnummer wordt vaak beschouwd als de eerste commerciële rap-hit en werd uitgebracht door The Sugarhill Gang in 1979?", 
            new List<string> { "Planet Rock", "The Message", "Delight", "White Lines" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level3, 
            "De Italiaanse componist Luigi Nono schreef de Il canto sospeso uit 1956, gebruikt teksten uit brieven van welke groep mensen?", 
            new List<string> { "Duitse vluchtelingen", "Oorlogs gevangenen", "Holocaust slachtoffers", "Communistische activisten" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Muziek, 
            Level.Level3, 
            "Welke baanbrekende elektronische muziekgroep uit Duitsland inspireerde genres zoals techno en hiphop, en bracht in 1974 het album Autobahn uit?", 
            new List<string> { "Tangerine Dream", "Can", "Neu!", "Kraftwerk" }, 
            3
        );
    }
 
    void SetDieren1()
    {
        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level1, 
            "Wat eet een panda meestal?", 
            new List<string> { "Boomschors", "Gras", "Bamboe", "Insecten" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level1, 
            "Welk dier staat bekend om zijn vermogen om van kleur te veranderen?", 
            new List<string> { "Octopus", "Kameleon", "Papegaai", "Zeepaardje" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level1, 
            "Welk zeedier staat bekend om zijn intelligentie?", 
            new List<string> { "Haai", "Zeester", "Zeepaardje", "Dolfijn" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level1, 
            "Welk dier is geen vogel?", 
            new List<string> { "Vlinder", "Kip", "Struisvogel", "Penguin" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level1, 
            "Welk dier leeft op de Noordpool en staat bekend om zijn dikke, witte vacht?", 
            new List<string> { "IJsbeer", "Grizzlybeer", "Koala", "Kangoeroe" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level1, 
            "Hoe noemt men een babyhond?", 
            new List<string> { "Welp", "Pup", "Kalf", "Lam" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level1, 
            "Welk dier legt eieren, maar is een zoogdier?", 
            new List<string> { "Kangoeroe", "Schildpad", "Vogelbekdier", "Penguin" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level1, 
            "Hoe heet het proces waarbij een rups verandert in een vlinder?", 
            new List<string> { "Kieming", "Metamorfose", "Fotosynthese", "Vermenigvuldiging" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level1, 
            "Wat is het grootste levende dier op aarde?", 
            new List<string> { "Afrikaanse Olifant", "Blauwe vinvis", "Reuzeninktvis", "De gele haarkwal" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level1, 
            "Wat is het snelste landdier ter wereld?", 
            new List<string> { "Tijger", "Jachtluipaard", "Paard", "Leeuw" }, 
            1
        );
    }

    void SetDieren2()
    {
        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level2, 
            "Welke giftige kwalsoort, staat bekend als een van de doodelijkste dieren op aarde, waarbij een mens geen overlevings kans heeft na vergiftiging?", 
            new List<string> { "Portugese oorlogsschip", "Australische Zeewesp", "Blauwe ringerige kwal", "Irukandji-kwal" }, 
            1
        );
        
        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level2, 
            "De axolotl, een soort salamander, heeft een opmerkelijk regeneratievermogen. Welke lichaamsdelen kan hij herstellen?", 
            new List<string> { "Botten en zenuwen", "Staart en poten", "Hart en hersenen", "Ogen en oren" }, 
            2
        );
        
        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level2, 
            "Welk dier heeft de sterkste bijtkracht, gemeten in kilogram per vierkante centimeter (kg/cm²)?", 
            new List<string> { "Konings Leeuw", "Nijlkrokodil", "Tijgerhaai", "piranha" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level2, 
            "Welke spin produceert een web dat tot 25 meter breed kan zijn, de breedste spinnenwebben in de dierenwereld?", 
            new List<string> { "Solifuug", "Gouden wielwebspin", "Nephila komaci", "Zeepspin" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level2, 
            "Hoe lang kan de zeekomkommer overleven zonder voedsel?", 
            new List<string> { "6 maanden", "1 Jaar", "1 jaar en 6 maanden", "2 jaar" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level2, 
            "Welke vogelsoort heeft het meest geavanceerde spraakvermogen, en kan menselijke woorden en geluiden perfect imiteren?", 
            new List<string> { "Kakapo", "Grijze roodstaartpapegaai", "Myna", "Kaketoe" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level2, 
            "Welk dier heeft het hoogste bloeddrukniveau van alle dieren op aarde?", 
            new List<string> { "Blauwvinvis", "Olifant", "Neushoorn", "Giraffe" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level2, 
            "Welke vogelsoort kan als enige ondersteboven vliegen en zelfs stil in de lucht hangen door snel met zijn vleugels te slaan?", 
            new List<string> { "Kolibrie", "Havik", "Arend", "Albatros" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level2, 
            "Wat is de langste geregistreerde levensduur van een Groenlandse haai, dat bekendstaat als een van de langst levende gewervelde dieren?", 
            new List<string> { "90 jaar", "190 jaar", "290 jaar", "390 jaar" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level2, 
            "Welk dier kan tot wel zes dagen onder water blijven zonder lucht te halen dankzij zijn grote longcapaciteit?", 
            new List<string> { "Zeeotter", "Potvis", "Blauwe vinvis", "Zeekoe" }, 
            1
        );
    }

    void SetDieren3()
    {
        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level3, 
            "Wat is het enige bekende dier dat zijn lichaamskleur permanent kan aanpassen aan de seizoenen door een speciaal proces van hormonale veranderingen?", 
            new List<string> { "Poolvos", "Sneeuwuil", "Toendrahaas", "Berggeit" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level3, 
            "Welke van de volgende dieren heeft een extreem zeldzaam “antifreeze”-eiwit in zijn bloed, dat voorkomt dat het bevriest bij temperaturen onder nul?", 
            new List<string> { "Antarctische tandbaars", "Groenlandse haai", "Kabeljauw", "Zalm" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level3, 
            "Welk dier heeft een maagzuur met een pH-waarde van bijna 1, waardoor het botten en vlees kan verteren die voor andere dieren onverteerbaar zijn?", 
            new List<string> { "Komodovaraan", "Python", "Tasmaanse duivel", "Hyena" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level3, 
            "Welke hagedissoort kan zowel bloed als lucht door zijn neusgaten naar buiten spuiten als verdediging tegen roofdieren?", 
            new List<string> { "Basielisk", "Hoornleguaan", "Blauwtongskink", "Kameleon" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level3, 
            "Welke in Zuid-Amerika voorkomende aapsoort heeft een speciaal dieet dat bestaat uit bepaalde vergiftigde bladeren die ze kunnen eten zonder nadelige gevolgen?", 
            new List<string> { "Brulaap", "Roodhandtamarin", "Stompneusaap", "Koning Lowietje" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level3, 
            "Wat is het enige zoogdier dat zijn lichaamstemperatuur bijna volledig kan verlagen om energie te besparen?", 
            new List<string> { "Kameel", "Woestijnrat", "Amerikaanse zwarte beer", "Dromedaris" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level3, 
            "Welke schildpadsoort kan een vorm van winterslaap in modderig water overleven door zuurstof uit water via zijn cloaca op te nemen?", 
            new List<string> { "Kaaimanschildpad", "Roodwangschildpad", "Australische bosschilpad", "Westelijke kousenbandshilpad" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level3, 
            "Welk dier heeft de hoogste lichaamstemperatuur in het dierenrijk, met een gemiddelde van ongeveer 41°C?", 
            new List<string> { "Noordse vleermuis", "Zwarte woestijnmier", "Kolibrie", "Sneeuwhaas" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level3, 
            "Welke inktvissoort kan licht produceren met behulp van bioluminescentie en heeft specifieke bacteriën die in speciale organen in zijn lichaam leven?", 
            new List<string> { "Sepia", "Octopoteuthidae", "Hawaïaanse bobtailinktvis", "Metasepia pfefferi" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Dieren, 
            Level.Level3, 
            "Welk insect is in staat om superkolonies te vormen die zich over duizenden kilometers uitstrekken en bestaat uit verschillende onderling verbonden nesten?", 
            new List<string> { "Termieten", "Aziatische Rode mier", "Argentijnse mier", "Japanse hoornaar" }, 
            2
        );
    }

    void SetVoetbal1()
    {
        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level1, 
            "De panenka is een... ?", 
            new List<string> { "Penalty", "Vrijetrap", "Hoekschop", "Schaarbeweging" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level1, 
            "Welk land heeft de meeste Wereldbeker titels?", 
            new List<string> { "Italië", "Duitsland", "Frankrijk", "Brazilië" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level1, 
            "Wie was de beste doelpuntenmaker in de Premier League in het seizoen 2022/2023?", 
            new List<string> { "Harry Kane", "Erling Haaland", "Mohamed Salah", "Cristiano Ronaldo" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level1, 
            "Welk stadion is van FC Barcelona?", 
            new List<string> { "Santiago Bernabéu", "Old Trafford", "Allianz Arena", "Camp Nou" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level1, 
            "Wie won de Ballon d'Or in 2023?", 
            new List<string> { "Lionel Messi", "Robert Lewandowski", "Kylian Mbappé", "Karim Benzema" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level1, 
            "Wat is de maximale speeltijd in een reguliere voetbalwedstrijd zonder extra tijd of verlengingen?", 
            new List<string> { "80 minuten", "90 minuten", "100 minuten", "120 minuten" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level1, 
            "Welke van de volgende landen heeft nooit de FIFA Wereldbeker gewonnen?", 
            new List<string> { "Nederland", "Duitsland", "Griekeland", "Argentinië" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level1, 
            "Hoeveel spelers staan er aan het begin van een normale voetbalwedstrijd op het veld voor elk team?", 
            new List<string> { "9", "10", "11", "12" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level1, 
            "Wie is de all-time topscorer van de Premier League", 
            new List<string> { "Wayne Rooney", "Sergio Agüero", "Alan Shearer", "Harry Kane" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level1, 
            "In welk land werd de allereerste Wereldbeker in 1930 gehouden?", 
            new List<string> { "Brazilië", "Frankrijk", "Uruguay", "Italië" }, 
            2
        );
    }

    void SetVoetbal2()
    {
        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "In welk jaar werd de UEFA Champions League voor het eerst gehouden onder de naam Champions League in plaats van Europese Cup?", 
            new List<string> { "1990", "1992", "1995", "1997" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "Wie is de enige speler die zowel de Ballon d'Or heeft gewonnen als de Gouden Schoen in hetzelfde seizoen?", 
            new List<string> { "Thierry Henry", "Marco van Basten", "Jean-Pierre Papin", "Cristiano Ronaldo" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "Welke club won de eerste editie van de Copa Libertadores in 1960?", 
            new List<string> { "Santos", "Peñarol", "Independiente", "Corinthians" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "Welke Afrikaanse speler scoorde het snelste doelpunt ooit in een FIFA Wereldbekerwedstrijd?", 
            new List<string> { "Hakan Şükür", "Roger Milla", "Asamoah Gyan", "Jean-Pierre Tokoto" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "Welke club heeft de meeste keer de Franse Ligue 1 gewonnen?", 
            new List<string> { "Lyon", "Marseille", "Paris Saint-Germain", "AS Saint-Étienne" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "Welke club had het langste ongeslagen seizoen in de Engelse topdivisie, met een record van 49 wedstrijden zonder verlies?", 
            new List<string> { "Manchester United", "Chelsea", "Arsenal", "Liverpool" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "Welke speler heeft het record voor het meeste aantal doelpunten gescoord in één seizoen van de Duitse Bundesliga?", 
            new List<string> { "Gerd Müller", "Robert Lewandowski", "Ulf Kirsten", "Klaus Fischer" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "In welk jaar vond de Miracle of Istanbul plaats, toen Liverpool een comeback maakte van 3-0 naar 3-3 in de UEFA Champions League finale?", 
            new List<string> { "2002", "2003", "2004", "2005" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "Welke speler was de eerste die meer dan 1000 professionele wedstrijden speelde voor één club in de Premier League?", 
            new List<string> { "Frank Lampard", "Steven Gerrard", "Ryan Giggs", "Paul Scholes" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "Welke club heeft de meeste keer de Coppa Italia gewonnen?", 
            new List<string> { "AS Roma", "Juventus", "AC Milan", "Napoli" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level2, 
            "Welke nationale ploeg behaalde als eerste de nummer 1-positie in de FIFA-ranglijst voor mannen?", 
            new List<string> { "Brazilë", "Italië", "Nederland", "Duitsland" }, 
            3
        );
    }

    void SetVoetbal3()
    {
        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level3, 
            "Welk land is het enige dat de Olympische gouden medaille heeft gewonnen in het voetbal en het daarna nooit opnieuw heeft gedaan?", 
            new List<string> { "Kosovo", "Tsjechoslowakije", "Soviet Unie", "West-Duitsland" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level3, 
            "Welke speler scoorde het allereerste doelpunt ooit in een officiële UEFA Cup-wedstrijd (nu Europa League) in het seizoen 1971/72?", 
            new List<string> { "Antonin Panenka", "Rainer Ohlhauser", "Willy Brokamp", "John Giles" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level3, 
            "Welke speler uit de vroege jaren 1900 had het record voor het meeste aantal doelpunten gescoord in de hoogste Engelse divisie?", 
            new List<string> { "Steve Bloomer", "Jimmy Greaves", "Dixie Dean", "Tommy Taylor" }, 
            0   
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level3, 
            "Wie was de allereerste speler die meer dan 500 doelpunten maakte in de Italiaanse Serie A?", 
            new List<string> { "Alessandro Del Piero", "Roberto Baggio", "Francesco Totti", "Giuseppe Meazza" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level3, 
            "Welk land heeft de meeste keer de FIFA Fair Play Trophy gewonnen?", 
            new List<string> { "Duitsland", "Brazilië", "Engeland", "Frankrijk" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level3, 
            "Welke club heeft het record voor het meeste aantal gespeelde seizoenen in de Engelse topdivisie zonder ooit te degraderen?", 
            new List<string> { "Arsenal", "Everton", "Chelsea", "Leicester City" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level3, 
            "Welke club was het eerste dat een treble (landstitel, nationale beker en Europese beker) won?", 
            new List<string> { "Ajax", "Celtic", "AS Saint-Étienne", "RSC Anderlecht" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level3, 
            "Welke club uit de Duitse Democratische Republiek is de enigste team die ooit de Europacup won?", 
            new List<string> { "FC Magdeburg", "SV 09 Stassfurt", "SC Germania-Jahn", "FuCC Viktoria" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level3, 
            "Welke speler heeft het record voor het meeste aantal doelpunten in de historie van de Braziliaanse competitie (Campeonato Brasileiro)?", 
            new List<string> { "Pelé", "Zico", "Romário", "Neymar Jr." }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Voetbal, 
            Level.Level3, 
            "Welke van de volgende voetballers heeft nooit voor een topclub in Europa gespeeld, ondanks zijn reputatie als een van de beste voetballers van zijn generatie?", 
            new List<string> { "George Weah", "Dejan Savićević", "Hristo Stoichkov", "Jari Litmanen" }, 
            0
        );
    }

    void SetGeschiedenis1()
    {
        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level1, 
            "In welk jaar werd de Verenigde Naties opgericht?", 
            new List<string> { "1918", "1945", "1955", "1975" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level1, 
            "Wat was de naam van het schip waarmee de Engelse ontdekkingsreiziger James Cook de wereld rondvoer?", 
            new List<string> { "Mayflower", "Endeavour", "Victoria", "Beagle" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level1, 
            "Wie was de beroemde vrouw die tijdens de Amerikaanse Burgeroorlog het rode kruis oprichtte?", 
            new List<string> { "Harriet Tubman", "Clara Barton", "Rosa Parks", "Eleanor Roosevelt" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level1, 
            "In welk jaar eindigde de Tweede Wereldoorlog?", 
            new List<string> { "1939", "1941", "1945", "1950" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level1, 
            "Welke oude beschaving bouwde de piramides?", 
            new List<string> { "Romeinen", "Grieken", "Egyptenaren", "Mesopotamiërs" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level1, 
            "Wie was de beroemde Italiaanse ontdekkingsreiziger die Amerika ontdekte in 1492?", 
            new List<string> { "Marco Polo", "Ferdinand Magellaan", "Christopher Columbus", "Vasco da Gama" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level1, 
            "Welke Franse leider werd keizer van Frankrijk na de Franse Revolutie?", 
            new List<string> { "Louis XVI", "Napoleon Bonaparte", "Charles de Gaulle", "Jean Jaurès" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level1, 
            "In welk land vond de beroemde Berlijnse Muur plaats?", 
            new List<string> { "Duitsland", "Frankrijk", "Engeland", "Italië" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level1, 
            "Wie was de Engelse koningin die het grootste deel van de 19e eeuw regeerde?", 
            new List<string> { "Elizabeth I", "Victoria", "Elizabeth II", "Mary I" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level1, 
            "In welk land vond de Slag bij Waterloo plaats, waarin Napoleon Bonaparte werd verslagen?   ", 
            new List<string> { "België", "Frankrijk", "Duitsland", "Nederland" }, 
            0
        );
    }

    void SetGeschiedenis2()
    {
        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level2, 
            "Wie was de uitvinder van de boekdrukkunst in Europa, bekend om zijn drukpers die de verspreiding van kennis bevorderde in de 15e eeuw?", 
            new List<string> { "Johann Gutenberg", "William Caxton", "Nikolaus Copernicus", "Johannes Kepler" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level2, 
            "In welke veldslag vesloeg Baldwin IV van Jeruzalem, Saladin in 1177, ondanks zijn ziekte en numerieke verschil van 5 tot 8 keer?", 
            new List<string> { "Slag bij Hattin", "Slag bij Montgisard", "Slag bij Arsuf", "Slag bij La Forbie" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level2, 
            "Welke beroemde Noorse ontdekkingsreiziger bereikte Noord-Amerika vijf eeuwen vóór Christoffel Columbus?", 
            new List<string> { "Thorfinn Heyerdahl", "Thor Heyerdahl", "Roald Amundsen", "Leif Erikson" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level2, 
            "Romeinse keizer voerde de hervormingen uit die leidden tot de stichting van de Romeinse provincies en het herstructureren van het leger in een professionele strijdmacht?", 
            new List<string> { "Augustus", "Titanius", "Trajanus", "Diocletianus" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level2, 
            "Welke Mongoolse leider was verantwoordelijk voor de verovering van Perzië en het vestigen van de Ilkhanat-dynastie?", 
            new List<string> { "Jebe Khan", "Kublai Khan", "Hulagu Khan", "Timur Khan" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level2, 
            "Welke Slavische vorst wordt erkend als de grondlegger van de Kievse Rijk in de 9e eeuw?", 
            new List<string> { "Vladimir de Grote", "Rurik of Hroerekr", "Oleg of Novgorod", "Yaroslav de Wijze" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level2, 
            "Wie was de eerste Egyptische farao die zich als god beschouwde en de goddelijke koninklijke macht centraliseerde?", 
            new List<string> { "Ramses II", "Amenhotep IV", "Tutankhamon", "Djoser" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level2, 
            "Wat was de eerste langdurige Europese koloniale bezetting in Afrika, die begon in de 15e eeuw?", 
            new List<string> { "Portugese bezittingen in Angola", "Spaanse bezittingen in Marokko", "Franse bezittingen in Senegal", "Portugese bezittingen in Guinea-Bissau" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level2, 
            "Welke bekende historische figuur was keizer van het Heilige Roomse Rijk tijdens de Slag bij Mohács in 1526?", 
            new List<string> { "Karel V", "Frederik de Grote", "Maximiliaan I", "Rudolf II" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level2, 
            "De Slag bij Lepanto in 1571 was een grote zeeslag tussen de Ottomaanse Turkse vloot en een christelijke coalitie. Welk land leidde deze christelijke coalitie?", 
            new List<string> { "Spanje", "Engeland", "Frankrijk", "Venetië" }, 
            0
        );
    }

    void SetGeschiedenis3()
    {
        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level3, 
            "Wat was de naam van het mystieke boek dat door de 17e-eeuwse Engelse alchemist John Dee werd gebruikt om met engelen te communiceren?", 
            new List<string> { "The Key of Solomon", "The Voynich Manuscript", "The Book of Soyga", "The Ripley Scroll" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level3, 
            "Welke Islamitische dynastie was verantwoordelijk voor de gouden eeuw van Bagdad?", 
            new List<string> { "De Abbasiden", "De Fatimiden", "De Umayyaden", "De Mamelukken" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level3, 
            "In welk conflict speelde de Oorlog van Jenkins' Oor een rol als aanleiding voor grotere conflicten tussen Spanje en Groot-Brittannië in de 18e eeuw?", 
            new List<string> { "De Zevenjarige Oorlog", "De Spaanse Successieoorlog", "De Oostenrijkse Successieoorlog", "De Dertienjarige Oorlog" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level3, 
            "Na welke oorlog werd bekende Griekse redenaar en politicus Demosthenes ter dood veroordeeld in 322 v.Chr.?", 
            new List<string> { "Zeeslag van Hellespont", "De Tweede Paraitakene", "De Argeaden Oorlog", "Lamische Oorlog" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level3, 
            "Wie was de Perzische koning die het Achaemenidische rijk uitbreidde en onder zijn bewind een van de eerste mensenrechtenverklaringen?", 
            new List<string> { "Darius I", "Cyrus I", "Artaxerxes II", "Xerxes I" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level3, 
            "Welke Russische tsaar was verantwoordelijk voor het bouwen van de stad Sint-Petersburg en het moderniseren van het Russische leger en bestuur?", 
            new List<string> { "Peter de Grote", " Ivan de Verschrikkelijke", "Catharina de Grote", "Alexander I" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level3, 
            "Welke filosoof uit de middeleeuwen schreef de tekst “The Consolation of Philosophy” terwijl hij gevangen zat?", 
            new List<string> { "Thomas van Aquino", "Boëthius", "Averroës", "Abelard" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level3, 
            "Welke Keltische stam in Gallië werd bekend door hun georganiseerde weerstand tegen de Romeinse veroveringen, met Vercingetorix als leider?", 
            new List<string> { "Helvetii", "Arverni", "Eburones", "Treveri" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level3, 
            "Welk document wordt beschouwd als de oudste vredesverdrag ter wereld, afgesloten tussen het Egyptische Rijk en het Hettitische Rijk?", 
            new List<string> { "Het Verdrag van Tordesillas", "Het Verdrag van Susa", "Het Verdrag van Kadesh", "Het Edict van Medinet Habu" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Geschiedenis, 
            Level.Level3, 
            "Welke Chinees admiraal leidde een reeks beroemde maritieme expedities door Zuidoost-Azië en Oost-Afrika tijdens de vroege Ming-dynastie?", 
            new List<string> { "Kublai Khan", "Sun Tzu", "Zheng He", "Li Shimin" }, 
            2
        );

    }

    void SetAstronomie1()
    {
        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level1, 
            "Hoeveel planeten heeft ons zonnestelsel sinds Pluto werd geclassificeerd als dwergplaneet?", 
            new List<string> { "7", "8", "9", "10" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level1, 
            "Wat is de naam van de grootste planeet in ons zonnestelsel?", 
            new List<string> { "Mars", "Aarde", "Jupiter", "Venus" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level1, 
            "Hoe heet het dichtstbijzijnde sterrenstelsel bij de Melkweg?", 
            new List<string> { "Triangulum-stelsel", "Sombrero-stelsel", "Andromeda-stelsel", "Cartwheel-stelsel" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level1, 
            "Welk hemellichaam draait om de Aarde?", 
            new List<string> { "Mars", "Pluto", "Zon", "Maan" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level1, 
            "Wat voor soort ster is de Zon?", 
            new List<string> { "Rode reus", "Witte dwerg", "Neutronenster", "Gele dwerg" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level1, 
            "Welke planeet staat het dichtst bij de Zon?", 
            new List<string> { "Saturnus", "Mars", "Mercurius", "Neptunus" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level1, 
            "Welke planeet staat bekend om zijn ringen?", 
            new List<string> { "Saturnus", "Jupiter", "Mars", "Neptunus" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level1, 
            "Welke planeet staat het verst van de Zon in ons zonnestelsel?", 
            new List<string> { "Uranus", "Mars", "Neptunus", "Jupiter" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level1, 
            "Hoe heet het punt in een baan waarbij een planeet het dichtst bij de Zon komt?", 
            new List<string> { "Aphelium", "Perihelium", "Zenith", "Nadir" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level1, 
            "Welke kracht zorgt ervoor dat planeten in een baan om de Zon blijven?", 
            new List<string> { "Magnetisme", "Kernfusie", "Zwaartekracht", "Wrijving" }, 
            2
        );
    }

    void SetAstronomie2()
    {
        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level2, 
            "Welke ster is de helderste ster in het sterrenbeeld Orion?", 
            new List<string> { "Rigel", "Betelgeuze", "Bellatrix", "Alnilam" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level2, 
            "Wat gebeurt er met tijd en ruimte in de buurt van een zwaar hemellichaam zoals een zwart gat, volgens de algemene relativiteitstheorie?", 
            new List<string> { "Tijd versnelt en ruimte zet uit", "Tijd blijft constant en ruimte trekt samen", "Tijd vertraagt en ruimte kromt", "Tijd versnelt en ruimte kromt" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level2, 
            "Hoe heet de schijfvormige structuur van gas en stof rond een nieuw ontstane ster, die uiteindelijk kan leiden tot de vorming van planeten?", 
            new List<string> { "Circumstellaire schijf", "Accretieschijf", "Protostellaire schijf", "Protoplanetaire schijf" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level2, 
            "Wat is de naam van het fenomeen waarbij het licht van een ster verschuift  als het zich van de waarnemer verwijdert?", 
            new List<string> { "Blauweverschuiving", "Gravitationele lenswerking", "Donkere energie", "Roodverschuiving" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level2, 
            "Welke beroemde ruimtewaarnemingsmissie heeft gegevens verzameld over de kosmische achtergrondstraling en gaf ons een nauwkeurige kaart van het vroege heelal?", 
            new List<string> { "Hubble", "Voyager", "WMAP", "Galileo" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level2, 
            "Wat is de naam van het fenomeen waarbij de zwaartekracht van een voorgrondobject het licht van een achtergrondobject buigt, wat de achtergrondobjecten helderder maakt?", 
            new List<string> { "Gravitationele lenswerking", "Zwaartekrachtgolven", "Kosmische uitdijing", "Zwaartekrachtsverschuiving" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level2, 
            "Wat is de naam van de stofrijke tussenstellaire wolken waar sterren worden geboren?", 
            new List<string> { "Supernova-restanten", "Planetaire nevels", "Moleculaire wolken", "Kosmische jets" }, 
            0
        );

                casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level2, 
            "Hoe wordt het punt genoemd waar een zwart gat de zwaartekracht zo sterk heeft dat zelfs licht er niet aan kan ontsnappen?", 
            new List<string> { "Singulariteit", "Event horizon", "Schwarzschild-radius", "Neutrino Antimaterie" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level2, 
            "Wat is de belangrijkste reden dat het oppervlak van Venus heter is dan dat van Mercurius, hoewel Mercurius dichter bij de Zon staat?", 
            new List<string> { "Venus heeft een dichte atmosfeer vol CO₂", "Venus draait langzamer rond haar as", "Venus heeft een sterke magnetosfeer", "Venus heeft een grotere massa dan Mercurius" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level2, 
            "Wat is de naam van de zone in ons zonnestelsel waar kleine, ijzige objecten in een schijfvormige regio zich bevinden, voorbij de baan van Neptunus?", 
            new List<string> { "Oortwolk", "Asteroïdengordel", "Heliopauze", "Kuipergordel" }, 
            3
        );
    }

    void SetAstronomie3()
    {
        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level3, 
            "Wat is het cruciale verschil in kernreactie dat massieve sterren in staat stelt elementen zwaarder dan koolstof te fuseren, in tegenstelling tot lichtere sterren zoals de zon?", 
            new List<string> { "Protonen-vangst", "S-process", "CNO-cyclus", "Neutronenbotsing" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level3, 
            "Wat is het specifieke element dat pas ontdekt werd door observaties van de Zon en dat lange tijd niet op aarde werd gevonden?", 
            new List<string> { "Helium", "Radium", "Uranium", "Neptunium" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level3, 
            "Wat zijn de objecten die het binnenste van zwarte gaten zouden kunnen vormen volgens sommige quantumzwaartekrachtmodellen?", 
            new List<string> { "Singularity Loops", "Gravitons", "Fuzzballs", "Quantum strings" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level3, 
            "Wat beschrijft de Tolman-Oppenheimer-Volkoff limiet?", 
            new List<string> { "De snelheid die massa aanneemt in een magnetisch veld", "De maximale massa van een neutronenster voordat hij een zwart gat wordt", "De minimumtijd die een ster heeft om een witte dwerg te vormen", "De afstand waarop een object een ander object niet meer kan inhalen" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level3, 
            "Welke formule beschrijft het proces van kernfusie in een ster, waarbij waterstof wordt omgezet in helium met massa-verlies en energieproductie als gevolg?", 
            new List<string> { "Schrödingervergelijking", "Bohr-model", "Bethe-Weizsäcker-cyclus", "Pauli-exclusieprincipe" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level3, 
            "Wat is de naam van het verre sterrenstelsel dat bekend staat als het eerste sterrenstelsel waarvan een gammaflits is waargenomen?", 
            new List<string> { "GRB 090423", "GN-z11", "IOK-1", "MACS0647-JD" }, 
            0
        );

                casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level3, 
            "Wat is de naam van de hypothese die stelt dat ons zonnestelsel een ster van het type “Primordial Black Hole” bevat als onontdekt object?", 
            new List<string> { "Planeta 9-hypothese", "Nibiru-theorie", "Nemesis-hypothese", "Primordial-hypothese" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level3, 
            "Wat beschrijft het concept van de “Big Rip” in kosmologie?", 
            new List<string> { "Het moment waarop zwaartekracht sterker wordt dan donkere energie", "Het uiteenvallen van ruimte en tijd door exponentiële expansie", "De samensmelting van het universum in één punt", "Een supernova-explosie die het universum zou vernietigen" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level3, 
            "Wat betekent de afkorting “LIGO”?", 
            new List<string> { "Laser Interferometer Gravitational Observatory", "Light Infrared Gamma-ray Observatory", "Large Interstellar Gamma Oscillation", "Long Integration Gravitational Opacity" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Astronomie, 
            Level.Level3, 
            "Wat is de belangrijkste oorzaak van gammaflitsen die honderden miljoenen lichtjaren ver kunnen reiken en extreem krachtige energie vrijmaken?", 
            new List<string> { "Botsingen tussen neutronensterren", "Ontploffing van superzware sterren", "Samentrekking van sterrem", "Jetuitbarstingen in zwarte gaten" }, 
            0
        );
    }

    void SetWetenschap1()
    {
        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level1, 
            "Hoeveel staten van materie zijn er traditioneel bekend?", 
            new List<string> { "2", "3", "4", "5" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level1, 
            "Wat is het chemische symbool voor water?", 
            new List<string> { "O₂", "CO₂", "H₂O", "NaCl" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level1, 
            "Wat voor soort gas ademen mensen in om te overleven?", 
            new List<string> { "Koolstofdioxide", "Waterstof", "Helium", "Zuurstof" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level1, 
            "Welke beroemde natuurkundige ontwikkelde de zwaartekrachtwet?", 
            new List<string> { "Albert Einstein", "Nikola Tesla", "Isaac Newton", "Galileo Galilei" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level1, 
            "Hoe heet de wetenschapper die de relativiteitstheorie ontwikkelde?", 
            new List<string> { "Marie Curie", "Albert Einstein", "Charles Darwin", "Stephen Hawking" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level1, 
            "Wat bestudeert een bioloog?", 
            new List<string> { "Sterren en planeten", "De oceaan", "Leven en organismen", "Zwaartekracht" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level1, 
            "Wat voor type cel bevat een kern?", 
            new List<string> { "Prokaryotische cel", "Bacteriële cel", "Virale cel", "Eukaryotische cel" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level1, 
            "Hoe heet het proces waarmee planten energie uit zonlicht halen?", 
            new List<string> { "Verdamping", "Condensatie", "Fotosynthese", "Osmose" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level1, 
            "Welke van de volgende elementen komt het meest voor in het universum?", 
            new List<string> { "Helium", "Zuurstof", "Waterstof", "Stikstof" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level1, 
            "Wat is de basiseenheid van leven?", 
            new List<string> { "Atomen", "Moleculen", "Cellen", "Organen" }, 
            2
        );
    }

    void SetWetenschap2()
    {
        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level2, 
            "Welke revolutionaire techniek wordt gebruikt om het DNA van organismen te wijzigen?", 
            new List<string> { "PCR", "Gel-electroforese", "CRISPR-Cas9", "ELISA" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level2, 
            "Welke subatomaire deeltjes bepalen vooral de chemische eigenschappen van een element?", 
            new List<string> { "Protonen", "Neutronen", "Positronen", "Elektronen" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level2, 
            "Welk element heeft het hoogste smeltpunt van alle metalen?", 
            new List<string> { "Wolfraam", "Gallium", "Titaan", "Platina" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level2, 
            "Welke wetenschapper is bekend om zijn werk met de kwantummechanica en de onzekerheidsrelatie?", 
            new List<string> { "Albert Einstein", "Werner Heisenberg", "Niels Bohr", "Max Planck" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level2, 
            "Wat beschrijft de tweede wet van de thermodynamica?", 
            new List<string> { "Energie kan niet worden vernietigd, alleen omgezet", "Het absolute nulpunt kan niet worden bereikt", "In een geïsoleerd systeem neemt de entropie toe", "Voor elke actie is er een gelijke en tegengestelde reactie" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level2, 
            "Wat is de naam van het apparaat dat werd gebruikt om de snelheid van een chemische reactie door gasverplaatsing te meten?", 
            new List<string> { "Manometer", "Barometer", "Spectrometer", "Diatomicmeter" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level2, 
            "Wat is de benaming voor de kracht die werkt tussen geladen deeltjes in elektromagnetische velden?", 
            new List<string> { "Nucleaire kracht", "Electrodynamics ", "Lorentzkracht", "Coulomb" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level2, 
            "Welke techniek wordt vaak gebruikt om de leeftijd van archeologische vondsten van organisch materiaal te bepalen?", 
            new List<string> { "Massaspectrometrie", "C14-datering", "Isotopen scheiding", "DecaFF" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level2, 
            "Wat is de naam van het effect dat optreedt wanneer een geluidsgolf in frequentie verandert als de bron beweegt ten opzichte van de waarnemer?", 
            new List<string> { "Interferentie", "Dopplereffect", "Resonantie", "Refractie" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level2, 
            "Hoeveel natuurlijke elementen bestaan er op het periodiek systeem?", 
            new List<string> { "92", "95", "97", "99" }, 
            0
        );
    }

    void SetWetenschap3()
    {
        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level3, 
            "Welk type elektromagnetische straling heeft de kortste golflengte en daardoor de meeste energie per foton?", 
            new List<string> { "Röntgenstralen", "Infraroodstralen", "Ultravioletstralen", "Gammastralen" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level3, 
            "Wat is de rol van Tachyon in theoretische fysica?", 
            new List<string> { "Hypothetische deeltjes die sneller bewegen dan het licht", "Deeltjes die massa verliezen naarmate ze versnellen", "Deeltjes zonder magnetisch veld", "Deeltjes die antimaterie in balans houden" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level3, 
            "Wat is de tweede wet van de thermodynamica en hoe beïnvloedt deze het universum?", 
            new List<string> { "Energie kan alleen in materie worden omgezet", "Energie in een gesloten systeem neemt altijd toe", "Entropie in een geïsoleerd systeem neemt altijd toe", "Temperatuur in een gesloten systeem blijft gelijk" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level3, 
            " In de biologie beschrijft het epigenoom een laag van genregulatie die onafhankelijk is van de DNA-sequentie. Welke moleculaire aanpassing is hiervan een voorbeeld?", 
            new List<string> { " DNA-duplicatie", "Histonmodificatie", "Mutaties in exonen", "Codon-optimalisatie" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level3, 
            "Welke hypothese verklaart het vreemde feit dat licht zich zowel als een deeltje als een golf gedraagt?", 
            new List<string> { "Bohr's complementariteitsprincipe", "Pauli-uitsluitingsprincipe", "Heisenberg-onzekerheidsprincipe", " Schrödingervergelijking" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level3, 
            "Wat is het wetenschappelijk begrip voor de “warmtedood” van het universum, waarbij het universum uiteindelijk geen thermische energie meer bevat?", 
            new List<string> { "Thermodynamisch impulsemoment", "Thermodynamisch evenwicht", "Thermodynamische potentiaal", "Thermodynamisch systeem" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level3, 
            "Wat betekent het in de wiskunde als een functie holomorf is?", 
            new List<string> { "Dat de functie differentieerbaar is op een complexe vlak", "Dat de functie een constante waarde heeft", "Dat de functie reëel is en alleen positieve waarden aanneemt", "Dat de functie afwisselende limieten heeft" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level3, 
            "Wat beschrijft het “Poincaré-conjectuur”, een belangrijk theorema in de topologie?", 
            new List<string> { "Elke veelvlak heeft dezelfde oppervlakte", "Elke bolvorm heeft een oneindige oppervlakte", "Elke kromme in het vlak kan zonder snijpunten getrokken worden", "Elk object dat lijkt op een 3-sfeer kan continu worden vervormd tot een bol" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.Wetenschap, 
            Level.Level3, 
            "Welk effect verklaart dat de rotatiesnelheid van de aarde langzaam afneemt door de invloed van de maan?", 
            new List<string> { "Maanbevingen", "Het Casimir-effect", "Getijdenwrijving", "De Lunar Prospector" }, 
            2
        );
    }

    void SetOverigeSporten1()
    {
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level1, 
            "Welke sport combineert skiën en schieten?", 
            new List<string> { "Snowboarden", "Biatlon", "Triatlon", "Langlaufen" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level1, 
            "Hoeveel spelers staan er standaard op het veld bij een volleybalteam?", 
            new List<string> { "5", "6", "7", "8" }, 
            1
        );
        
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level1, 
            "Welke kleur kaart krijg je in tennis als je een fout maakt?", 
            new List<string> { "Geel", "Rood", "Tennis kent geen kaarten", "Blauw" }, 
            2
        );
        
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level1, 
            "Hoeveel punten krijg je voor een try in rugby?", 
            new List<string> { "3", "4", "5", "6" }, 
            1
        );
        
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level1, 
            "Welke van deze sporten gebruikt een puck?", 
            new List<string> { "IJshockey", "Waterpolo", "Squash", "Handbal" }, 
            0
        );
        
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level1, 
            "In badminton, hoe heet het wanneer je direct de shuttle over het net slaat zodat je tegenstander hem niet kan raken?", 
            new List<string> { "Smash", "Drop", "Lob", "Serve" }, 
            0
        );
        
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level1, 
            "Hoeveel doelpunten telt een basketbalteam per basket?", 
            new List<string> { "1 of 2", "2 of 3", "1 of 3", "1, 2 of 3" }, 
            3
        );
        
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level1, 
            "Welke sport wordt beoefend op een paard en combineert dressuur, springen en cross-country?", 
            new List<string> { "Polo", "Eventing", "Springconcours", "Fullcourse" }, 
            1
        );
        
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level1, 
            "In welke sport is Usain Bolt wereldberoemd geworden?", 
            new List<string> { "Zwemmen", "Atletiek", "Gymnastiek", "Wielrennen" }, 
            1
        );
        
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level1, 
            "Hoeveel spelers zwemmen er in een waterpoloteam in het water tijdens een wedstrijd?", 
            new List<string> { "5", "6", "7", "8" }, 
            2
        );
    }

    void SetOverigeSporten2()
    {
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level2, 
            "Wat is de minimale toegestane reactie-tijd voor een start in een 100 meter sprint bij internationale wedstrijden?", 
            new List<string> { "0.05 secondes", "0.10 secondes", "0.15 secondes", "0.20 secondes" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level2, 
            "Welke autosportcategorie werd in 1986 wereldwijd verboden vanwege het extreem hoge risico op dodelijke ongelukken voor zowel coureurs als toeschouwers?", 
            new List<string> { "Wangan midnight race", "Peak's Mountain downhill", "LeMans prototype", "Group B rally" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level2, 
            "Welke bochtpositie is verplicht om te behouden tijdens het short-track schaatsen om diskwalificatie te voorkomen?", 
            new List<string> { "Buitenbocht rechterbeen naar buiten", "Buitenbocht hand op knie", "Binnenbocht rechterbeen naar buiten", "Binnenbocht hand op knie" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level2, 
            "Bij de schoolslag is er een specifieke regel voor de benen. Welke beweging is niet toegestaan?", 
            new List<string> { "Een dolphin kick", "Een frog kick", "Een whip kick", "Een flutter kick" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level2, 
            "Bij waterpolo, wat is de maximale tijd die een speler het balbezit mag hebben zonder te schieten of passen?", 
            new List<string> { "20 secondes", "25 secondes", "30 secondes", "35 secondes" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level2, 
            "Wat is de moeilijkheidsgraad van een oefening die een dubbele salto met twist bevat in turnen op de vloer volgens de FIG-code?", 
            new List<string> { "E", "F", "G", "H" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level2, 
            "Wie had in het seizoen 2022–2023 het hoogste gemiddelde aantal assists per wedstrijd in de NBA?", 
            new List<string> { "LeBron James", "James Harden", "Chris Paul", "Terry Rose" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level2, 
            "In judo, hoeveel waza-ari scores zijn nodig om een wedstrijd te winnen zonder ippon?", 
            new List<string> { "1", "2", "3", "4" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level2, 
            "Wat betekent het “starboard right-of-way” principe in zeilen?", 
            new List<string> { "Boot aan bakboord gaat voor", "Boot aan stuurboord gaat voor", "Boot die achter ligt gaat voor", "Boot die voor ligt moet stoppen" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level2, 
            "Hoeveel schijven moet een biatleet raken tijdens een staande schietronde van vijf schoten?", 
            new List<string> { "3", "5", "7", "9" }, 
            1
        );
    }

    void SetOverigeSporten3()
    {
        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level3, 
            "Wat is de maximale hellingshoek van een skihelling die wordt gebruikt voor Olympische alpineskiën?", 
            new List<string> { "30 graden", "35 graden", "40 graden", "45 graden" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level3, 
            "Welke biomechanische factor is het meest cruciaal voor het genereren van kracht tijdens een 100 meter sprint?", 
            new List<string> { "Stapfrequentie", "Staplengte", "Grondreactie", "Lichaamshoek" }, 
            2
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level3, 
            "Welke fysiologische aanpassing treedt op bij duursporters na langdurige training op grote hoogte?", 
            new List<string> { "Verhoogde bloedcellen", "Verhoogde spiermassa", "Verbeterde anaerobe capaciteit", "Verhoogde vetverbranding" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level3, 
            "Welke regel maakt dat een aanval in het sabre-schermen direct geldig is bij contact, wannneer de tegenstander ook een aanval starte op hetzelfde moment?", 
            new List<string> { "Prioriteitsregel", "Timingregel", "Touchregel", "Counterregel" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level3, 
            "Welke vrouwelijke turnster was de eerste ooit die een perfect 10 kreeg op de Olympische Spelen in 1976?", 
            new List<string> { "Olga Korbut", "Larisa Latynina", "Simone Biles", "Nadia Comăneci" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level3, 
            "Wat is een “freeze” in curling?", 
            new List<string> { "De steen stopt exact tegen de achterste steen aan", "De steen blijft op de hog line liggen", "De steen wordt volledig geblokkeerd door een andere steen", "De steen wordt verwijderd van het huis" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level3, 
            "Bij Olympisch boksen mogen deelnemers vanaf welk jaar maximaal 3 minuten per ronde boksen?", 
            new List<string> { "Vanaf 16 jaar", "Vanaf 17 jaar", "Vanaf 18 jaar", "Vanaf 19 jaar" }, 
            1
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level3, 
            "Wat is het maximale aantal pogingen dat een klimmer heeft voor één boulderprobleem bij een internationale wedstrijd?", 
            new List<string> { "4", "5", "6", "Geen limiet" }, 
            3
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level3, 
            "Wie was de eerste man die ooit meer dan 500 kg totaal tilde in een gewichtheflingwedstrijd?", 
            new List<string> { "Vasily Alekseyev", "Naim Süleymanoğlu", "Leonid Taranenko", "Pyrros Dimas" }, 
            0
        );

        casinoData.AddQuestion(
            Subject.OverigeSporten, 
            Level.Level3, 
            "Hoe lang duurt een standaard polowedstrijd?", 
            new List<string> { "4 chuckers van 15 minuten", "6 chukkers van 7 minuten", "8 chukkers van 10 minuten", "3 chukkers van 20 minuten" }, 
            1
        );
    }

    public void Load()
    {
        casinoData = this.gameObject.GetComponent<CasinoData>();

        SetMuziek1();
        SetMuziek2();
        SetMuziek3();

        SetDieren1();
        SetDieren2();
        SetDieren3();

        SetVoetbal1();
        SetVoetbal2();
        SetVoetbal3();

        SetGeschiedenis1();
        SetGeschiedenis2();
        SetGeschiedenis3();

        SetAstronomie1();
        SetAstronomie2();
        SetAstronomie3();

        SetWetenschap1();
        SetWetenschap2();
        SetWetenschap3();

        SetOverigeSporten1();
        SetOverigeSporten2();
        SetOverigeSporten3();
    }
}
