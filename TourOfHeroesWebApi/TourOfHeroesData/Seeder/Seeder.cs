using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TourOfHeroesData.Models;
using TourOfHeroesData.Seeder.Contracts;

namespace TourOfHeroesData.Seeder
{
    public class Seeder : ISeeder
    {
        private readonly TourOfHeroesDbContext _dbContext;

        public Seeder(TourOfHeroesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedDatabase()
        {
            var heroList = new List<Hero>();

            if (!_dbContext.Heroes.Any())
            {
                var cptAmerica = new Hero
                {
                    Name = "Captain America",
                    Description = "Captain America is a fictional superhero appearing in American comic books published by Marvel Comics. Created by cartoonists Joe Simon and Jack Kirby, the character first appeared in Captain America Comics #1 (cover dated March 1941) from Timely Comics, a predecessor of Marvel Comics. Captain America was designed as a patriotic supersoldier who often fought the Axis powers of World War II and was Timely Comics' most popular character during the wartime period. The popularity of superheroes waned following the war and the Captain America comic book was discontinued in 1950, with a short-lived revival in 1953. Since Marvel Comics revived the character in 1964, Captain America has remained in publication.",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707816/HeroUploads/cpt_america_img_h3vbyt.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568818599/HeroUploads/cpt-america-cover_mcbtj7.jpg",
                    RealName = "Chris Evans",
                    Birthday = new DateTime(1981,6,13).ToString("MM/dd/yyyy"),
                    Gender = "Male"
                };

                heroList.Add(cptAmerica);

                var spiderman = new Hero
                {
                    Name = "Spider-Man",
                    Description = "Spider-Man is a fictional superhero created by writer-editor Stan Lee and writer-artist Steve Ditko. He first appeared in the anthology comic book Amazing Fantasy #15 (August 1962) in the Silver Age of Comic Books. He appears in American comic books published by Marvel Comics, as well as in a number of movies, television shows, and video game adaptations set in the Marvel Universe. In the stories, Spider-Man is the alias of Peter Parker, an orphan raised by his Aunt May and Uncle Ben in New York City after his parents Richard and Mary Parker were killed in a plane crash. Lee and Ditko had the character deal with the struggles of adolescence and financial issues, and accompanied him with many supporting characters, such as J. Jonah Jameson, Harry Osborn, Max Modell, romantic interests Gwen Stacy and Mary Jane Watson, and foes such as Doctor Octopus, Green Goblin and Venom. His origin story has him acquiring spider-related abilities after a bite from a radioactive spider; these include clinging to surfaces, shooting spider-webs from wrist-mounted devices, and detecting danger with his \"spider-sense\".",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/spiderman-img_vcfskb.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/spiderman-cover_akceyp.jpg",
                    RealName = "Tobey Maguire",
                    Birthday = new DateTime(1975, 6, 27).ToString("MM/dd/yyyy"),
                    Gender = "Male"
                };

                heroList.Add(spiderman);

                var thor = new Hero
                {
                    Name = "Thor",
                    Description = "Thor is a 2011 American superhero film based on the Marvel Comics character of the same name, produced by Marvel Studios and distributed by Paramount Pictures.[N 1] It is the fourth film in the Marvel Cinematic Universe (MCU). The film was directed by Kenneth Branagh, written by the writing team of Ashley Edward Miller and Zack Stentz along with Don Payne, and stars Chris Hemsworth as the title character, alongside Natalie Portman, Tom Hiddleston, Stellan Skarsgård, Colm Feore, Ray Stevenson, Idris Elba, Kat Dennings, Rene Russo, and Anthony Hopkins. The film sees Thor banished to Earth from Asgard, stripped of his powers and his hammer Mjölnir, after reigniting a dormant war. As his brother Loki plots to take the Asgardian throne, Thor must prove himself worthy.",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707791/HeroUploads/thor-img_aeb3pa.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707792/HeroUploads/thor-cover_ab0mdt.jpg",
                    RealName = "Chris Hemsworth",
                    Birthday = new DateTime(1983, 8, 11).ToString("MM/dd/yyyy"),
                    Gender = "Male"
                };

                heroList.Add(thor);

                var hulk = new Hero
                {
                    Name = "Hulk",
                    Description = "Hulk is a 2003 American superhero film based on the fictional Marvel Comics character of the same name directed by Ang Lee which stars Eric Bana as the title character, Jennifer Connelly as Betty Ross, Sam Elliott as General Thaddeus E. \"Thunderbolt\" Ross, Josh Lucas, and Nick Nolte as Bruce's father. The film explores the origins of Bruce Banner, who after a lab accident involving gamma radiation finds himself able to turn into a huge green-skinned monster whenever he is emotionally provoked or stressed, while he is pursued by the United States military and comes into a conflict with his biological father, who has his own dark agenda for his son.",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707689/HeroUploads/hulk-img_dauwaa.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/hulk-cover_mgxrcq.jpg",
                    RealName = "Mark Ruffalo",
                    Birthday = new DateTime(1967, 11, 22).ToString("MM/dd/yyyy"),
                    Gender = "Male"
                };

                heroList.Add(hulk);

                var ironman = new Hero
                {
                    Name = "Iron Man",
                    Description = "Iron Man is a 2008 American superhero film based on the Marvel Comics character of the same name, produced by Marvel Studios and distributed by Paramount Pictures. The first installment of the Marvel Cinematic Universe, it was directed by Jon Favreau from a screenplay by Mark Fergus and Hawk Ostby, and Art Marcum and Matt Holloway. The film follows Tony Stark (Robert Downey Jr.), an industrialist and master engineer who builds a mechanized suit of armor and becomes the superhero Iron Man.",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707689/HeroUploads/ironman-img_kvxuvy.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707689/HeroUploads/ironman-cover_kvdzuy.jpg",
                    RealName = "Robert Downey Jr",
                    Birthday = new DateTime(1965, 4, 4).ToString("MM/dd/yyyy"),
                    Gender = "Male"
                };

                heroList.Add(ironman);

                var hawkeye = new Hero
                {
                    Name = "Hawkeye",
                    Description = "Hawkeye (Clinton Francis \"Clint\" Barton) is a fictional superhero appearing in American comic books published by Marvel Comics. Created by writer Stan Lee and artist Don Heck, the character first appeared as a villain in Tales of Suspense #57 (Sept. 1964) and later joined the Avengers in The Avengers #16 (May 1965). He has been a prominent member of the team ever since. He was also ranked at #44 on IGN's Top 100 Comic Book Heroes list.[3]",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/hawkeye-img_lvvkth.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/hawkeye-cover_dpzgsd.jpg",
                    RealName = "Jeremy Renner",
                    Birthday = new DateTime(1971, 1, 7).ToString("MM/dd/yyyy"),
                    Gender = "Male"
                };

                heroList.Add(hawkeye);

                var blackwidow = new Hero
                {
                    Name = "Black Widow",
                    Description = "Black Widow is an upcoming American superhero film based on the Marvel Comics character of the same name. Produced by Marvel Studios and distributed by Walt Disney Studios Motion Pictures, it is intended to be the twenty-fourth film in the Marvel Cinematic Universe (MCU). The film is directed by Cate Shortland, written by Jac Schaeffer and Ned Benson, and stars Scarlett Johansson as Natasha Romanoff / Black Widow, alongside David Harbour, Florence Pugh, O-T Fagbenle, and Rachel Weisz.",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568712498/HeroUploads/blackwidow-img_kvu6ch.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568712498/HeroUploads/blackwidow-cover_kuhthy.jpg",
                    RealName = "Scarlett Johansson",
                    Birthday = new DateTime(1984, 11, 22).ToString("MM/dd/yyyy"),
                    Gender = "Female"
                };

                heroList.Add(blackwidow);

                var blackpanther = new Hero
                {
                    Name = "Black Panther",
                    Description = "Black Panther is a 2018 American superhero film based on the Marvel Comics character of the same name. Produced by Marvel Studios and distributed by Walt Disney Studios Motion Pictures, it is the eighteenth film in the Marvel Cinematic Universe (MCU). The film is directed by Ryan Coogler, who co-wrote the screenplay with Joe Robert Cole, and stars Chadwick Boseman as T'Challa / Black Panther, alongside Michael B. Jordan, Lupita Nyong'o, Danai Gurira, Martin Freeman, Daniel Kaluuya, Letitia Wright, Winston Duke, Angela Bassett, Forest Whitaker, and Andy Serkis. In Black Panther, T'Challa is crowned king of Wakanda following his father's death, but his sovereignty is challenged by an adversary who plans to abandon the country's isolationist policies and begin a global revolution.",
                    Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568712497/HeroUploads/black-panther-img_ea6z3e.jpg",
                    CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568712498/HeroUploads/black-panther-cover_gulfqb.jpg",
                    RealName = "Chadwick Boseman",
                    Birthday = new DateTime(1976, 11, 29).ToString("MM/dd/yyyy"),
                    Gender = "Male"
                };

                heroList.Add(blackpanther);

                _dbContext.Heroes.AddRange(heroList);
                _dbContext.SaveChangesAsync();
            }
        }
    }
}
