using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Metrix.Cmp
{
    class WowInfo
    {
        public static Dictionary<string, string> Encounters = new Dictionary<string, string>();
        public static Dictionary<string, string> BossSpells = new Dictionary<string, string>();
        public static Dictionary<string, string> Affixes = new Dictionary<string, string>();
        public static void MTX_InitEncountersDictionaries()
        {
            // Encounters
            Encounters.Add("2086", "Rezan");
            Encounters.Add("2084", "Alunza");
            Encounters.Add("2085", "Volkaal");
            Encounters.Add("2087", "Yazma");
            Encounters.Add("2139", "Golden Serpent");
            Encounters.Add("2140", "Council of Tribes");
            Encounters.Add("2143", "King Dazar");
            Encounters.Add("2094", "Council o' Captains");
            Encounters.Add("2095", "Ring of Booty");
            Encounters.Add("2096", "Harlan Sweete");
            Encounters.Add("2093", "Skycaptain Kragg");
            Encounters.Add("2101", "Sandqueen");
            Encounters.Add("2102", "Jess Howlis");
            Encounters.Add("2103", "Valyri");
            Encounters.Add("2104", "Korgus");
            Encounters.Add("2257", "Tussle Tonks");
            Encounters.Add("2258", "KUJ0");
            Encounters.Add("2259", "Machinist's Garden");
            Encounters.Add("2260", "King Mechagon");
            Encounters.Add("2290", "King Gobbamak");
            Encounters.Add("2312", "Trixie & Naemo");
            Encounters.Add("2292", "Gunker");
            Encounters.Add("2291", "HK8 Unit");
            Encounters.Add("2111", "Elder Leaxa");
            Encounters.Add("2118", "Infested Crag");
            Encounters.Add("2112", "Sporecaller Zancha");
            Encounters.Add("2123", "Unbound Abomination");
            Encounters.Add("2105", "Crowd Pummeler");
            Encounters.Add("2106", "Azerokk");
            Encounters.Add("2107", "Rixxa Fluxflame");
            Encounters.Add("2108", "Mogul Razzdunk");
            Encounters.Add("2113", "Heartsbane Triad");
            Encounters.Add("2115", "Raal the Gluttonous");
            Encounters.Add("2114", "Soulbound Goliath");
            Encounters.Add("2116", "The Waycrests");
            Encounters.Add("2117", "Gorak Tul");
            Encounters.Add("2130", "Agusirr");
            Encounters.Add("2132", "Lord Stormsong");
            Encounters.Add("2131", "Tidesage Council");
            Encounters.Add("2133", "Volzith");
            Encounters.Add("2124", "Adderis & Aspix");
            Encounters.Add("2127", "Avatar of Sethraliss");
            Encounters.Add("2126", "Galvazzt");
            Encounters.Add("2125", "Merektha");
            Encounters.Add("2100", "Viq'Goth");
            Encounters.Add("2099", "Hadal Darkfathom");
            Encounters.Add("2109", "Captain Lockwood");
            Encounters.Add("2097", "Chopper Bainbridge");
            Encounters.Add("2098", "Chopper Redhook");

            // Bosses Spells

            BossSpells.Add("255371", "Fear");
            BossSpells.Add("255421", "Devour");
            BossSpells.Add("255582", "Molten Gold");
            BossSpells.Add("277072", "Corrupted Gold");
            BossSpells.Add("259572", "Noxious Stench");
            BossSpells.Add("250096", "Wracking Pain");
            BossSpells.Add("250036", "Shadowy Remains");
            BossSpells.Add("265781", "Serpentine Gust");
            BossSpells.Add("267874", "Burning Corruption");
            BossSpells.Add("267763", "Wretched Discharge");
            BossSpells.Add("266231", "Severing Axe");
            BossSpells.Add("267273", "Poison Nova");
            BossSpells.Add("266951", "Barrel Through");
            BossSpells.Add("268403", "Gale Slash");
            BossSpells.Add("272902", "Chain Shot");
            BossSpells.Add("256589", "Barrel Smash");
            BossSpells.Add("267533", "Whirlpool of Blades");
            BossSpells.Add("267522", "Cutting Surge");
            BossSpells.Add("256358", "Shark Toss");
            BossSpells.Add("256405", "Shark Tornado");
            BossSpells.Add("257305", "Cannon Barrage");
            BossSpells.Add("272046", "Dive Bomb");
            BossSpells.Add("256106", "Powder Shot");
            BossSpells.Add("257092", "Sand Trap");
            BossSpells.Add("257777", "Crippling Shiv");
            BossSpells.Add("257791", "Fear");
            BossSpells.Add("257785", "Flashing Daggers");
            BossSpells.Add("256955", "Cinderflame");
            BossSpells.Add("257028", "Fuselighter");
            BossSpells.Add("256970", "Ignition");
            BossSpells.Add("256083", "Crossfire");
            BossSpells.Add("263345", "Massive Blast");
            BossSpells.Add("282945", "Buzz Saws");
            BossSpells.Add("285152", "Foe Flipper");
            BossSpells.Add("294929", "Blazing Chomp");
            BossSpells.Add("291930", "Air Drop");
            BossSpells.Add("291949", "Venting Flames");
            BossSpells.Add("285460", "Disco-bomb-ulator");
            BossSpells.Add("294954", "Self-Trimming Hedge");
            BossSpells.Add("294869", "Roaring Flame");
            BossSpells.Add("291915", "Plasma Orb");
            BossSpells.Add("297256", "Charged Smash");
            BossSpells.Add("302681", "Mega Taze");
            BossSpells.Add("298626", "Pedal to the Metal");
            BossSpells.Add("298946", "Roadkill");
            BossSpells.Add("298947", "Roadkill");
            BossSpells.Add("298571", "Burnout");
            BossSpells.Add("298259", "Gooped");
            BossSpells.Add("302384", "Static Discharge");
            BossSpells.Add("303206", "Annihilation Blast");
            BossSpells.Add("260879", "Blood Bolt");
            BossSpells.Add("261498", "Creeping Rot");
            BossSpells.Add("264757", "Sanguine Feast");
            BossSpells.Add("260685", "Taint of G'hun");
            BossSpells.Add("260793", "Indigestion");
            BossSpells.Add("260292", "Charge");
            BossSpells.Add("259718", "Upheaval");
            BossSpells.Add("269843", "Vile Expulsion");
            BossSpells.Add("273226", "Decaying Spores");
            BossSpells.Add("269301", "Putrid Blood");
            BossSpells.Add("257337", "Shocking Claw");
            BossSpells.Add("275907", "Tectonic Smash");
            BossSpells.Add("258628", "Resonant Quake");
            BossSpells.Add("257544", "Jagged Cut");
            BossSpells.Add("259853", "Chemical Burnout");
            BossSpells.Add("259940", "Propellant Blast");
            BossSpells.Add("259022", "Azerite Catalyst");
            BossSpells.Add("260202", "Drill Smash");
            BossSpells.Add("260280", "Gatling Gun");
            BossSpells.Add("260811", "Homing Missle");
            BossSpells.Add("260703", "Unstable Mark");
            BossSpells.Add("268086", "Aura of Dread");
            BossSpells.Add("260700", "Ruinous Bolt");
            BossSpells.Add("260701", "Bramble Bolt");
            BossSpells.Add("260699", "Soul Bolt");
            BossSpells.Add("264923", "Tenderize");
            BossSpells.Add("264698", "Rotten Expulsion");
            BossSpells.Add("260551", "Soul Thorns");
            BossSpells.Add("260547", "Burning Brush");
            BossSpells.Add("268308", "Discordant Cadenza");
            BossSpells.Add("268271", "Wracking Chord");
            BossSpells.Add("266225", "Darkened Lightning");
            BossSpells.Add("268202", "Death Lens");
            BossSpells.Add("273658", "Dark Leap");
            BossSpells.Add("264560", "Choking Waters");
            BossSpells.Add("264155", "Surging Rush");
            BossSpells.Add("268347", "Void Bolt");
            BossSpells.Add("268896", "Mind Rend");
            BossSpells.Add("269104", "Explosive Void");
            BossSpells.Add("267818", "Slicing Blast");
            BossSpells.Add("267899", "Hindering Cleave");
            BossSpells.Add("267385", "Tentacle Slam");
            BossSpells.Add("313363", "Tentacle Slam");
            BossSpells.Add("267809", "Consume Essence");
            BossSpells.Add("267459", "Consume Essence");
            BossSpells.Add("269419", "Yawning Gate");
            BossSpells.Add("267360", "Gasp of the Sunken City");
            BossSpells.Add("263318", "Jolt");
            BossSpells.Add("268851", "Lightning Shield");
            BossSpells.Add("263573", "Cyclone Strike");
            BossSpells.Add("263374", "Conduction");
            BossSpells.Add("263775", "Gust");
            BossSpells.Add("263425", "Arc Dash");
            BossSpells.Add("269686", "Plague Toads");
            BossSpells.Add("268061", "Chain Lightning");
            BossSpells.Add("268008", "Snake Charm");
            BossSpells.Add("257431", "Meat Hook");
            BossSpells.Add("257326", "Gore Crash");
            BossSpells.Add("257292", "Heavy Slash");
            BossSpells.Add("269029", "Clear the Deck");
            BossSpells.Add("268230", "Crimson Swipe");
            BossSpells.Add("268260", "Broadside");
            BossSpells.Add("261565", "Crashing Tide");
            BossSpells.Add("257883", "Break Water");
            BossSpells.Add("276068", "Tidal Surge");
            BossSpells.Add("275014", "Putrid Waters");
            BossSpells.Add("269266", "Slam - Tentacle");
            BossSpells.Add("263914", "Blinding Sand");
            BossSpells.Add("272657", "Noxious Breath");
            BossSpells.Add("264206", "Burrow");
            BossSpells.Add("267027", "Cyrotoxin");
            BossSpells.Add("266939", "Barrel Through r1");
            BossSpells.Add("269230", "Hunting Leap");
            BossSpells.Add("109128", "Charge r1");
            BossSpells.Add("265914", "Molten Gold");
            BossSpells.Add("268796", "Impaling Spear");
            BossSpells.Add("256200", "Heartstopping Venom");
            BossSpells.Add("256044", "Deadeye");
            BossSpells.Add("256105", "Explosive Burst");
            BossSpells.Add("256710", "Exploding Barrels");
            BossSpells.Add("257119", "Sand Trap");
            BossSpells.Add("268387", "Virulent Pathogen");
            BossSpells.Add("268234", "Bile Expulsion");


            // Affixes

            Affixes.Add("240446", "Explosive");
            Affixes.Add("240447", "Quaking");
            Affixes.Add("226510", "Sanguine");
            Affixes.Add("240559", "Griveous");
            Affixes.Add("209862", "Volcanic");
            Affixes.Add("243237", "Bursting");
        }
        public static string MTX_SetBossNameByID(string bossID)
        {
            string bossName = "";

            foreach(var bossid in Encounters)
            {
                if(bossid.Key == bossID)
                {
                    bossName = bossid.Value;
                }
            }

            return bossName;
        }

        public static string MTX_SetAffixByID(string affixID)
        {
            string affixName = "";

            foreach (var affid in Affixes)
            {
                if (affid.Key == affixID)
                {
                    affixName = affid.Value;
                }
            }

            return affixName;
        }

        public static string MTX_SetBossMechanicByID(string mechanicID)
        {
            string mechanicName = "";

            foreach(var mechanic in BossSpells)
            {
                if (mechanic.Key == mechanicID)
                {
                    mechanicName = mechanic.Value;
                }
            }
                
            return mechanicName;
        }
    }
}
