// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
namespace TourOfHeroesData.Seeder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TourOfHeroesCommon;

    using Models;
    using Contracts;

    using Microsoft.AspNetCore.Identity;

    public class Seeder : ISeeder
    {
        private readonly TourOfHeroesDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public Seeder(TourOfHeroesDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public void SeedDatabase()
        {
            _dbContext.Database.EnsureCreated();

            // Seed user roles

            #region Roles

            if (!_dbContext.Roles.Any())
            {
                _dbContext.Roles.Add(new IdentityRole
                {
                    Name = GlobalConstants.AdminRole,
                    NormalizedName = GlobalConstants.AdminRole.ToUpper()
                });

                _dbContext.Roles.Add(new IdentityRole
                {
                    Name = GlobalConstants.EditorRole,
                    NormalizedName = GlobalConstants.EditorRole.ToUpper()
                });

                _dbContext.Roles.Add(new IdentityRole
                {
                    Name = GlobalConstants.UserRole,
                    NormalizedName = GlobalConstants.UserRole.ToUpper()
                });
            }

            #endregion

            // Seed users

            #region Users

            string adminId;

            if (!_dbContext.ApplicationUsers.Any())
            {
                const string userPassword = "1234";

                #region AdminUser

                const string adminPassword = "admin123";
                var admin = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Admin Adminov",
                    JobTitle = "CTO",
                    RegisteredOn = DateTime.Now
                };

                _userManager.CreateAsync(admin, adminPassword).Wait();
                _userManager.AddToRoleAsync(admin, GlobalConstants.AdminRole).Wait();
                adminId = admin.Id;

                #endregion

                #region User

                var user = new ApplicationUser()
                {
                    UserName = "kelesha",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Gosho Goshev",
                    JobTitle = "Digital Marketing Expert",
                    RegisteredOn = DateTime.Now.AddDays(1)
                };

                _userManager.CreateAsync(user, userPassword).Wait();
                _userManager.AddToRoleAsync(user, GlobalConstants.UserRole).Wait();

                #endregion

                #region User1

                var user1 = new ApplicationUser()
                {
                    UserName = "kelesha1",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Valio Goshev",
                    JobTitle = "CEO",
                    RegisteredOn = DateTime.Now.AddDays(2)
                };

                _userManager.CreateAsync(user1, userPassword).Wait();
                _userManager.AddToRoleAsync(user1, GlobalConstants.UserRole).Wait();

                #endregion

                #region User2

                var user2 = new ApplicationUser()
                {
                    UserName = "kelesha2",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Ivan Basmata",
                    JobTitle = "SEO Specialist",
                    RegisteredOn = DateTime.Now.AddDays(3)
                };

                _userManager.CreateAsync(user2, userPassword).Wait();
                _userManager.AddToRoleAsync(user2, GlobalConstants.UserRole).Wait();

                #endregion

                #region User3

                var user3 = new ApplicationUser()
                {
                    UserName = "kelesha3",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Avram Bakalina",
                    JobTitle = "PR Manager",
                    RegisteredOn = DateTime.Now.AddDays(4)
                };

                _userManager.CreateAsync(user3, userPassword).Wait();
                _userManager.AddToRoleAsync(user3, GlobalConstants.UserRole).Wait();

                #endregion

                #region User4

                var user4 = new ApplicationUser()
                {
                    UserName = "kelesha4",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Qnko Babacha",
                    JobTitle = "Software Engineer",
                    RegisteredOn = DateTime.Now.AddDays(5)
                };

                _userManager.CreateAsync(user4, userPassword).Wait();
                _userManager.AddToRoleAsync(user4, GlobalConstants.UserRole).Wait();

                #endregion

                #region User5

                var user5 = new ApplicationUser()
                {
                    UserName = "kelesha5",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Stamat Purvanov",
                    JobTitle = "Data Operator",
                    RegisteredOn = DateTime.Now.AddDays(6)
                };

                _userManager.CreateAsync(user5, userPassword).Wait();
                _userManager.AddToRoleAsync(user5, GlobalConstants.UserRole).Wait();

                #endregion

                #region User6

                var user6 = new ApplicationUser()
                {
                    UserName = "kelesha6",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Grigor Dimitrov",
                    JobTitle = "Junior Software Developer",
                    RegisteredOn = DateTime.Now.AddDays(7)
                };

                _userManager.CreateAsync(user6, userPassword).Wait();
                _userManager.AddToRoleAsync(user6, GlobalConstants.UserRole).Wait();

                #endregion

                #region User7

                var user7 = new ApplicationUser()
                {
                    UserName = "kelesha7",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Tabastqn Kirkorov",
                    JobTitle = "Product Manager",
                    RegisteredOn = DateTime.Now.AddDays(8)
                };

                _userManager.CreateAsync(user7, userPassword).Wait();
                _userManager.AddToRoleAsync(user7, GlobalConstants.UserRole).Wait();

                #endregion

                #region User8

                var user8 = new ApplicationUser()
                {
                    UserName = "kelesha8",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Gerasim Trifonov",
                    JobTitle = "Human Resources",
                    RegisteredOn = DateTime.Now.AddDays(9)
                };

                _userManager.CreateAsync(user8, userPassword).Wait();
                _userManager.AddToRoleAsync(user8, GlobalConstants.UserRole).Wait();

                #endregion

                #region User9

                var user9 = new ApplicationUser()
                {
                    UserName = "kelesha9",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Hristiyan Kostadinov",
                    JobTitle = "Sales Manager",
                    RegisteredOn = DateTime.Now.AddDays(10)
                };

                _userManager.CreateAsync(user9, userPassword).Wait();
                _userManager.AddToRoleAsync(user9, GlobalConstants.UserRole).Wait();

                #endregion

                #region User10

                var user10 = new ApplicationUser()
                {
                    UserName = "kelesha10",
                    Email = "asdasd@abv.bg",
                    ProfileImage = "https://icon-library.net/images/no-profile-picture-icon/no-profile-picture-icon-7.jpg",
                    FullName = "Asparuh Ivanov",
                    JobTitle = "Marketing Coordinator",
                    RegisteredOn = DateTime.Now.AddDays(11)
                };

                _userManager.CreateAsync(user10, userPassword).Wait();
                _userManager.AddToRoleAsync(user10, GlobalConstants.UserRole).Wait();

                #endregion
            }
            else
            {
                var id = this._userManager.FindByNameAsync("admin").Result.Id;
                adminId = id;
            }

            #endregion

            // Seed blog posts

            #region Blogs

            if (!_dbContext.Blogs.Any())
            {
                var blogList = new List<Blog>();

                #region LastAirBenderSeeder

                var lastAirbender = new Blog()
                {
                    AuthorUserName = "admin",
                    BlogImage = "http://res.cloudinary.com/goofy5752/image/upload/v1573027191/BlogImages/ttiksj99xv302ivzle90.jpg",
                    Content = "<p style=\"margin: 0.5em 0px; font-size: 14px; background-color: rgb(255, 255, 255);\"><span style=\"font-size: 13px; background-color: rgba(185, 185, 185, 0.1);\"><b style=\"\"><i style=\"\"><font color=\"#ff0080\" style=\"\" face=\"Comic Sans MS\">The world is divided into four kingdoms, each represented by the element they harness, and peace has lasted throughout the realms of Water, Air, Earth, and Fire under the supervision of the Avatar, a link to the spirit world and the only being capable of mastering the use of all four elements. When young Avatar Aang disappears, the Fire Nation launches an attack to eradicate all members of the Air Nomads to prevent interference in their future plans for world domination. 100 years pass and current Fire Lord Ozai continues to conquer and imprison anyone with elemental \"bending\" abilities in the Earth and Water Kingdoms, while siblings Katara and Sokka from a Southern Water Tribe find a mysterious boy trapped beneath the ice outside their village. Upon rescuing him, he reveals himself to be Aang, Avatar and last of the Air Nomads. Swearing to protect the Avatar, Katara and Sokka journey with him to the Northern Water Kingdom in his quest to master \"Waterbending\" and eventually fulfill</font></i></b></span><br></p>",
                    Title = "The last airbender",
                    PublishedOn = DateTime.Now,
                    UserId = adminId
                };

                blogList.Add(lastAirbender);

                #endregion

                #region JokerSeeder

                var joker = new Blog()
                {
                    AuthorUserName = "admin",
                    BlogImage = "http://res.cloudinary.com/goofy5752/image/upload/v1573028032/BlogImages/gu1hkuzljeyk1t1kkjt3.jpg",
                    Content = "<p style=\"margin: 0.5em 0px; font-size: 14px; background-color: rgb(255, 255, 255);\"><b style=\"\"><i style=\"\"><font face=\"Calibri\" style=\"\" color=\"#ff80ff\">Joker&nbsp;is a 2019 American&nbsp;<a href=\"https://en.wikipedia.org/wiki/Psychological_thriller\" title=\"Psychological thriller\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">psychological thriller</a>&nbsp;film directed by&nbsp;<a href=\"https://en.wikipedia.org/wiki/Todd_Phillips\" title=\"Todd Phillips\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Todd Phillips</a>, who co-wrote the screenplay with&nbsp;<a href=\"https://en.wikipedia.org/wiki/Scott_Silver\" title=\"Scott Silver\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Scott Silver</a>. The film, based on&nbsp;<a href=\"https://en.wikipedia.org/wiki/DC_Comics\" title=\"DC Comics\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">DC Comics</a>&nbsp;characters, stars&nbsp;<a href=\"https://en.wikipedia.org/wiki/Joaquin_Phoenix\" title=\"Joaquin Phoenix\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Joaquin Phoenix</a>&nbsp;as the&nbsp;<a href=\"https://en.wikipedia.org/wiki/Joker_(character)\" title=\"Joker (character)\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Joker</a>. An&nbsp;<a href=\"https://en.wikipedia.org/wiki/Origin_story\" title=\"Origin story\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">origin story</a>&nbsp;set in 1981, the film follows Arthur Fleck, a failed stand-up comedian who turns to a life of crime and chaos in&nbsp;<a href=\"https://en.wikipedia.org/wiki/Gotham_City\" title=\"Gotham City\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Gotham City</a>.&nbsp;<a href=\"https://en.wikipedia.org/wiki/Robert_De_Niro\" title=\"Robert De Niro\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Robert De Niro</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Zazie_Beetz\" title=\"Zazie Beetz\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Zazie Beetz</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Frances_Conroy\" title=\"Frances Conroy\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Frances Conroy</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Brett_Cullen\" title=\"Brett Cullen\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Brett Cullen</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Glenn_Fleshler\" title=\"Glenn Fleshler\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Glenn Fleshler</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Bill_Camp\" title=\"Bill Camp\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Bill Camp</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Shea_Whigham\" title=\"Shea Whigham\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Shea Whigham</a>, and&nbsp;<a href=\"https://en.wikipedia.org/wiki/Marc_Maron\" title=\"Marc Maron\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Marc Maron</a>&nbsp;appear in supporting roles.&nbsp;Joker&nbsp;was produced by&nbsp;<a href=\"https://en.wikipedia.org/wiki/Warner_Bros._Pictures\" class=\"mw-redirect\" title=\"Warner Bros. Pictures\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Warner Bros. Pictures</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/DC_Films\" title=\"DC Films\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">DC Films</a>, and Joint Effort in association with&nbsp;<a href=\"https://en.wikipedia.org/wiki/Bron_Studios\" title=\"Bron Studios\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Bron Creative</a>&nbsp;and&nbsp;<a href=\"https://en.wikipedia.org/wiki/Village_Roadshow_Pictures\" title=\"Village Roadshow Pictures\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Village Roadshow Pictures</a>, and distributed by Warner Bros.</font></i></b></p><p style=\"margin: 0.5em 0px; color: rgb(34, 34, 34); font-size: 14px; background-color: rgb(255, 255, 255);\"><b style=\"\"><i style=\"\"><font face=\"Comic Sans MS\">Phillips conceived&nbsp;Joker&nbsp;in 2016 and wrote the script with Silver throughout 2017. The two were inspired by 1970s character studies and the films of&nbsp;<a href=\"https://en.wikipedia.org/wiki/Martin_Scorsese\" title=\"Martin Scorsese\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Martin Scorsese</a>, who was initially attached to the project as a producer. The graphic novel&nbsp;<a href=\"https://en.wikipedia.org/wiki/Batman:_The_Killing_Joke\" title=\"Batman: The Killing Joke\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Batman: The Killing Joke</a>&nbsp;(1988) was the basis for the premise, but Phillips and Silver otherwise did not look to specific comics for inspiration. Phoenix became attached in February 2018 and was cast that July, while the majority of the cast signed on by August.&nbsp;<a href=\"https://en.wikipedia.org/wiki/Principal_photography\" title=\"Principal photography\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Principal photography</a>&nbsp;took place in&nbsp;<a href=\"https://en.wikipedia.org/wiki/New_York_City\" title=\"New York City\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">New York City</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Jersey_City\" class=\"mw-redirect\" title=\"Jersey City\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Jersey City</a>, and&nbsp;<a href=\"https://en.wikipedia.org/wiki/Newark,_New_Jersey\" title=\"Newark, New Jersey\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Newark</a>, from September to December 2018.&nbsp;Joker&nbsp;is the first live-action&nbsp;<a href=\"https://en.wikipedia.org/wiki/Batman_in_film\" title=\"Batman in film\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">theatrical&nbsp;Batman&nbsp;film</a>&nbsp;to receive an&nbsp;<a href=\"https://en.wikipedia.org/wiki/Motion_Picture_Association_of_America_film_rating_system#MPAA_film_ratings\" title=\"Motion Picture Association of America film rating system\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">R-rating</a>&nbsp;from the&nbsp;<a href=\"https://en.wikipedia.org/wiki/Motion_Picture_Association_of_America\" title=\"Motion Picture Association of America\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Motion Picture Association of America</a>, due to its violent and disturbing content.</font></i></b></p><p style=\"margin: 0.5em 0px; color: rgb(34, 34, 34); font-size: 14px; background-color: rgb(255, 255, 255);\"><font face=\"Times New Roman\"><u><i style=\"\">Joker</i>&nbsp;premiered at the&nbsp;<a href=\"https://en.wikipedia.org/wiki/76th_Venice_International_Film_Festival\" title=\"76th Venice International Film Festival\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">76th Venice International Film Festival</a>&nbsp;on August 31, 2019, where it won the&nbsp;<a href=\"https://en.wikipedia.org/wiki/Golden_Lion\" title=\"Golden Lion\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Golden Lion</a>, and was released in the United States on October 4, 2019. The film polarized critics; while Phoenix's performance was praised, the dark tone, portrayal of mental illness, and handling of violence divided responses.<sup id=\"cite_ref-7\" class=\"reference\" style=\"line-height: 1; unicode-bidi: isolate; white-space: nowrap; font-size: 11.2px;\"><a href=\"https://en.wikipedia.org/wiki/Joker_(2019_film)#cite_note-7\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">[7]</a></sup>&nbsp;<i style=\"\">Joker</i>&nbsp;also generated concerns of inspiring real-world violence; the movie theater where the&nbsp;<a href=\"https://en.wikipedia.org/wiki/2012_Aurora,_Colorado_mass_shooting\" class=\"mw-redirect\" title=\"2012 Aurora, Colorado mass shooting\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">2012 Aurora, Colorado mass shooting</a>&nbsp;occurred during a screening of&nbsp;<i style=\"\"><a href=\"https://en.wikipedia.org/wiki/The_Dark_Knight_Rises\" title=\"The Dark Knight Rises\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">The Dark Knight Rises</a></i>&nbsp;refused to show it. The film became an unexpected hit and set box office records for an October release and has grossed $938 million worldwide, making it the&nbsp;<a href=\"https://en.wikipedia.org/wiki/2019_in_film#Highest-grossing_films\" title=\"2019 in film\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">seventh highest-grossing film of 2019</a>&nbsp;and the&nbsp;<a href=\"https://en.wikipedia.org/wiki/List_of_highest-grossing_R-rated_films\" title=\"List of highest-grossing R-rated films\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">highest-grossing R-rated film</a>&nbsp;of all time.</u></font></p>",
                    Title = "Joker",
                    PublishedOn = DateTime.Now,
                    UserId = adminId
                };

                blogList.Add(joker);

                #endregion

                #region AvengersSeeder

                var avengers = new Blog()
                {
                    AuthorUserName = "admin",
                    BlogImage = "http://res.cloudinary.com/goofy5752/image/upload/v1573028842/BlogImages/qt6kbf1voloqb8feygqs.jpg",
                    Content = "<p style=\"margin: 0.5em 0px; color: rgb(34, 34, 34); font-size: 14px; background-color: rgb(255, 255, 255);\"><b style=\"\"><i style=\"\"><font face=\"Calibri\">Marvel's The Avengers<sup id=\"cite_ref-filmfestivaltraveler_7-0\" class=\"reference\" style=\"line-height: 1; unicode-bidi: isolate; white-space: nowrap; font-size: 11.2px;\"><a href=\"https://en.wikipedia.org/wiki/The_Avengers_(2012_film)#cite_note-filmfestivaltraveler-7\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">[6]</a></sup>&nbsp;(classified under the name&nbsp;Marvel Avengers Assemble&nbsp;in the United Kingdom and Ireland),<sup id=\"cite_ref-BBFC_4-1\" class=\"reference\" style=\"line-height: 1; unicode-bidi: isolate; white-space: nowrap; font-size: 11.2px;\"><a href=\"https://en.wikipedia.org/wiki/The_Avengers_(2012_film)#cite_note-BBFC-4\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">[3]</a></sup><sup id=\"cite_ref-IFCO_8-0\" class=\"reference\" style=\"line-height: 1; unicode-bidi: isolate; white-space: nowrap; font-size: 11.2px;\"><a href=\"https://en.wikipedia.org/wiki/The_Avengers_(2012_film)#cite_note-IFCO-8\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">[7]</a></sup>&nbsp;or simply&nbsp;The Avengers, is a 2012 American&nbsp;<a href=\"https://en.wikipedia.org/wiki/Superhero_film\" title=\"Superhero film\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">superhero film</a>&nbsp;based on the&nbsp;<a href=\"https://en.wikipedia.org/wiki/Marvel_Comics\" title=\"Marvel Comics\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Marvel Comics</a>&nbsp;superhero team&nbsp;<a href=\"https://en.wikipedia.org/wiki/Avengers_(comics)\" title=\"Avengers (comics)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">of the same name</a>, produced by&nbsp;<a href=\"https://en.wikipedia.org/wiki/Marvel_Studios\" title=\"Marvel Studios\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Marvel Studios</a>&nbsp;and distributed by&nbsp;<a href=\"https://en.wikipedia.org/wiki/Walt_Disney_Studios_Motion_Pictures\" title=\"Walt Disney Studios Motion Pictures\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Walt Disney Studios Motion Pictures</a>.<sup id=\"cite_ref-Paramount_3-1\" class=\"reference\" style=\"line-height: 1; unicode-bidi: isolate; white-space: nowrap; font-size: 11.2px;\"><a href=\"https://en.wikipedia.org/wiki/The_Avengers_(2012_film)#cite_note-Paramount-3\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">[N 1]</a></sup>&nbsp;It is&nbsp;<a href=\"https://en.wikipedia.org/wiki/List_of_Marvel_Cinematic_Universe_films\" title=\"List of Marvel Cinematic Universe films\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">the sixth film</a>&nbsp;in the&nbsp;<a href=\"https://en.wikipedia.org/wiki/Marvel_Cinematic_Universe\" title=\"Marvel Cinematic Universe\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Marvel Cinematic Universe</a>&nbsp;(MCU). The film was written and directed by&nbsp;<a href=\"https://en.wikipedia.org/wiki/Joss_Whedon\" title=\"Joss Whedon\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Joss Whedon</a>&nbsp;and features an&nbsp;<a href=\"https://en.wikipedia.org/wiki/Ensemble_cast\" title=\"Ensemble cast\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">ensemble cast</a>&nbsp;that includes&nbsp;<a href=\"https://en.wikipedia.org/wiki/Robert_Downey_Jr.\" title=\"Robert Downey Jr.\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Robert Downey Jr.</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Chris_Evans_(actor)\" title=\"Chris Evans (actor)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Chris Evans</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Mark_Ruffalo\" title=\"Mark Ruffalo\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Mark Ruffalo</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Chris_Hemsworth\" title=\"Chris Hemsworth\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Chris Hemsworth</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Scarlett_Johansson\" title=\"Scarlett Johansson\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Scarlett Johansson</a>, and&nbsp;<a href=\"https://en.wikipedia.org/wiki/Jeremy_Renner\" title=\"Jeremy Renner\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Jeremy Renner</a>&nbsp;as the titular Avengers team, alongside&nbsp;<a href=\"https://en.wikipedia.org/wiki/Tom_Hiddleston\" title=\"Tom Hiddleston\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Tom Hiddleston</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Clark_Gregg\" title=\"Clark Gregg\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Clark Gregg</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Cobie_Smulders\" title=\"Cobie Smulders\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Cobie Smulders</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Stellan_Skarsg%C3%A5rd\" title=\"Stellan Skarsgård\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Stellan Skarsgård</a>, and&nbsp;<a href=\"https://en.wikipedia.org/wiki/Samuel_L._Jackson\" title=\"Samuel L. Jackson\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Samuel L. Jackson</a>. In the film,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Nick_Fury\" title=\"Nick Fury\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Nick Fury</a>, director of the spy agency&nbsp;<a href=\"https://en.wikipedia.org/wiki/S.H.I.E.L.D.\" title=\"S.H.I.E.L.D.\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">S.H.I.E.L.D.</a>, recruits&nbsp;<a href=\"https://en.wikipedia.org/wiki/Tony_Stark_(Marvel_Cinematic_Universe)\" title=\"Tony Stark (Marvel Cinematic Universe)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Tony Stark</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Steve_Rogers_(Marvel_Cinematic_Universe)\" title=\"Steve Rogers (Marvel Cinematic Universe)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Steve Rogers</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Bruce_Banner_(Marvel_Cinematic_Universe)\" title=\"Bruce Banner (Marvel Cinematic Universe)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Bruce Banner</a>, and&nbsp;<a href=\"https://en.wikipedia.org/wiki/Thor_(Marvel_Cinematic_Universe)\" title=\"Thor (Marvel Cinematic Universe)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Thor</a>&nbsp;to form a team that must stop Thor's brother&nbsp;<a href=\"https://en.wikipedia.org/wiki/Loki_(comics)\" title=\"Loki (comics)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Loki</a>&nbsp;from subjugating Earth.</font></i></b></p><p style=\"margin: 0.5em 0px; color: rgb(34, 34, 34); font-size: 14px; background-color: rgb(255, 255, 255);\"><font face=\"Times New Roman\"><i>The film's development began when Marvel Studios received a loan from&nbsp;<a href=\"https://en.wikipedia.org/wiki/Merrill_Lynch\" title=\"Merrill Lynch\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Merrill Lynch</a>&nbsp;in April 2005. After the success of the film&nbsp;<a href=\"https://en.wikipedia.org/wiki/Iron_Man_(2008_film)\" title=\"Iron Man (2008 film)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Iron Man</a>&nbsp;in May 2008, Marvel announced that&nbsp;The Avengers&nbsp;would be released in July 2011. With the signing of Johansson in March 2009, the film was pushed back for a 2012 release. Whedon was brought on board in April 2010 and rewrote the original screenplay by&nbsp;<a href=\"https://en.wikipedia.org/wiki/Zak_Penn\" title=\"Zak Penn\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Zak Penn</a>. Production began in April 2011 in Albuquerque, New Mexico, before moving to Cleveland, Ohio, in August and New York City in September. The film was converted to 3D in post-production.</i></font></p><p style=\"margin: 0.5em 0px; color: rgb(34, 34, 34); font-size: 14px; background-color: rgb(255, 255, 255);\"><font face=\"Times New Roman\"><i>The Avengers&nbsp;premiered at Hollywood's&nbsp;<a href=\"https://en.wikipedia.org/wiki/El_Capitan_Theatre\" title=\"El Capitan Theatre\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">El Capitan Theatre</a>&nbsp;on April 11, 2012, and was released in the United States on May 4, 2012. The film received praise for Whedon's direction and screenplay, visual effects, action sequences, acting, and musical score, and garnered&nbsp;<a href=\"https://en.wikipedia.org/wiki/List_of_accolades_received_by_Marvel%27s_The_Avengers\" class=\"mw-redirect\" title=\"List of accolades received by Marvel's The Avengers\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">numerous awards and nominations</a>&nbsp;including&nbsp;<a href=\"https://en.wikipedia.org/wiki/Academy_Award\" class=\"mw-redirect\" title=\"Academy Award\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Academy Award</a>&nbsp;and&nbsp;<a href=\"https://en.wikipedia.org/wiki/British_Academy_of_Film_and_Television_Arts\" title=\"British Academy of Film and Television Arts\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">BAFTA</a>&nbsp;nominations for achievements in visual effects. It set or tied numerous box office records, including the&nbsp;<a href=\"https://en.wikipedia.org/wiki/List_of_biggest_opening_weekends\" class=\"mw-redirect\" title=\"List of biggest opening weekends\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">biggest opening weekend</a>&nbsp;in the United States and Canada.&nbsp;The Avengers&nbsp;grossed over $1.5 billion worldwide and became the&nbsp;<a href=\"https://en.wikipedia.org/wiki/List_of_highest-grossing_films\" title=\"\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">third-highest-grossing film of all time</a>, as well as the&nbsp;<a href=\"https://en.wikipedia.org/wiki/2012_in_film\" title=\"2012 in film\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">highest-grossing film of 2012</a>. It is the first Marvel production to generate $1 billion in ticket sales. In 2017, it was featured as one of the 100 greatest films of all time in&nbsp;<a href=\"https://en.wikipedia.org/wiki/Empire_(film_magazine)\" title=\"Empire (film magazine)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Empire</a>&nbsp;magazine's poll of&nbsp;<a href=\"https://en.wikipedia.org/wiki/Empire_(film_magazine)#The_100_Greatest_Movies\" title=\"Empire (film magazine)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">The 100 Greatest Movies</a>.</i></font></p><p style=\"margin: 0.5em 0px; font-family: sans-serif; font-size: 14px; background-color: rgb(255, 255, 255);\"><font color=\"#ff0080\">Three sequels, titled&nbsp;<i style=\"\"><a href=\"https://en.wikipedia.org/wiki/Avengers:_Age_of_Ultron\" title=\"Avengers: Age of Ultron\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Avengers: Age of Ultron</a></i>,&nbsp;<i style=\"\"><a href=\"https://en.wikipedia.org/wiki/Avengers:_Infinity_War\" title=\"Avengers: Infinity War\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Avengers: Infinity War</a></i>, and&nbsp;<i style=\"\"><a href=\"https://en.wikipedia.org/wiki/Avengers:_Endgame\" title=\"Avengers: Endgame\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Avengers: Endgame</a></i>, were released in May 2015, April 2018, and April 2019, respectively.</font></p>",
                    Title = "Avengers",
                    PublishedOn = DateTime.Now,
                    UserId = adminId
                };

                blogList.Add(avengers);

                #endregion

                #region CaptainMarvelSeeder

                var captainMarvel = new Blog()
                {
                    AuthorUserName = "admin",
                    BlogImage = "http://res.cloudinary.com/goofy5752/image/upload/v1573028969/BlogImages/zzhv1f4lolnkqxefx0l2.jpg",
                    Content = "<p style=\"margin: 0.5em 0px; color: rgb(34, 34, 34); font-size: 14px; background-color: rgb(255, 255, 255);\"><i style=\"\"><u style=\"\"><font face=\"Times New Roman\"><b style=\"\">Captain Marvel</b>&nbsp;is a 2019 American&nbsp;<a href=\"https://en.wikipedia.org/wiki/Superhero_film\" title=\"Superhero film\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">superhero film</a>&nbsp;based on the&nbsp;<a href=\"https://en.wikipedia.org/wiki/Marvel_Comics\" title=\"Marvel Comics\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Marvel Comics</a>&nbsp;character&nbsp;<a href=\"https://en.wikipedia.org/wiki/Carol_Danvers\" title=\"Carol Danvers\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Carol Danvers</a>. Produced by&nbsp;<a href=\"https://en.wikipedia.org/wiki/Marvel_Studios\" title=\"Marvel Studios\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Marvel Studios</a>&nbsp;and distributed by&nbsp;<a href=\"https://en.wikipedia.org/wiki/Walt_Disney_Studios_Motion_Pictures\" title=\"Walt Disney Studios Motion Pictures\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Walt Disney Studios Motion Pictures</a>, it is&nbsp;<a href=\"https://en.wikipedia.org/wiki/List_of_Marvel_Cinematic_Universe_films\" title=\"List of Marvel Cinematic Universe films\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">the twenty-first film</a>&nbsp;in the&nbsp;<a href=\"https://en.wikipedia.org/wiki/Marvel_Cinematic_Universe\" title=\"Marvel Cinematic Universe\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Marvel Cinematic Universe</a>&nbsp;(MCU). The film is written and directed by&nbsp;<a href=\"https://en.wikipedia.org/wiki/Anna_Boden_and_Ryan_Fleck\" title=\"Anna Boden and Ryan Fleck\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Anna Boden and Ryan Fleck</a>, with&nbsp;<a href=\"https://en.wikipedia.org/wiki/Geneva_Robertson-Dworet\" title=\"Geneva Robertson-Dworet\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Geneva Robertson-Dworet</a>&nbsp;also contributing to the screenplay.&nbsp;<a href=\"https://en.wikipedia.org/wiki/Brie_Larson\" title=\"Brie Larson\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Brie Larson</a>&nbsp;stars as Danvers, alongside&nbsp;<a href=\"https://en.wikipedia.org/wiki/Samuel_L._Jackson\" title=\"Samuel L. Jackson\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Samuel L. Jackson</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Ben_Mendelsohn\" title=\"Ben Mendelsohn\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Ben Mendelsohn</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Djimon_Hounsou\" title=\"Djimon Hounsou\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Djimon Hounsou</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Lee_Pace\" title=\"Lee Pace\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Lee Pace</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Lashana_Lynch\" title=\"Lashana Lynch\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Lashana Lynch</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Gemma_Chan\" title=\"Gemma Chan\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Gemma Chan</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Annette_Bening\" title=\"Annette Bening\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Annette Bening</a>,&nbsp;<a href=\"https://en.wikipedia.org/wiki/Clark_Gregg\" title=\"Clark Gregg\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Clark Gregg</a>, and&nbsp;<a href=\"https://en.wikipedia.org/wiki/Jude_Law\" title=\"Jude Law\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Jude Law</a>. Set in 1995, the story follows Danvers as she becomes&nbsp;<a href=\"https://en.wikipedia.org/wiki/Captain_Marvel_(Marvel_Comics)\" title=\"Captain Marvel (Marvel Comics)\" style=\"color: rgb(11, 0, 128); background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Captain Marvel</a>&nbsp;after Earth is caught in the center of a galactic conflict between two alien civilizations.</font></u></i></p><p style=\"margin: 0.5em 0px; font-size: 14px; background-color: rgb(255, 255, 255);\"><font color=\"#ff8000\" style=\"\" face=\"Comic Sans MS\"><b style=\"\">Development of the film began as early as May 2013. It was officially announced in October 2014 as Marvel Studios' first female-led superhero film.&nbsp;<a href=\"https://en.wikipedia.org/wiki/Nicole_Perlman\" title=\"Nicole Perlman\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Nicole Perlman</a>&nbsp;and&nbsp;<a href=\"https://en.wikipedia.org/wiki/Meg_LeFauve\" title=\"Meg LeFauve\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Meg LeFauve</a>&nbsp;were hired as a writing team the following April after submitting separate takes on the character. The story borrows elements from&nbsp;<a href=\"https://en.wikipedia.org/wiki/Roy_Thomas\" title=\"Roy Thomas\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Roy Thomas</a>'s 1971 \"<a href=\"https://en.wikipedia.org/wiki/Kree%E2%80%93Skrull_War\" title=\"Kree–Skrull War\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Kree–Skrull War</a>\" comic book storyline. Larson was announced as Danvers at the 2016&nbsp;<a href=\"https://en.wikipedia.org/wiki/San_Diego_Comic-Con\" title=\"San Diego Comic-Con\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">San Diego Comic-Con</a>, with Boden and Fleck hired to direct in April 2017. Robertson-Dworet soon took over scripting duties, with the remainder of the cast added by the start of filming.&nbsp;<a href=\"https://en.wikipedia.org/wiki/Location_shooting\" title=\"Location shooting\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">Location shooting</a>&nbsp;began in January 2018, with&nbsp;<a href=\"https://en.wikipedia.org/wiki/Principal_photography\" title=\"Principal photography\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">principal photography</a>&nbsp;beginning that March in California before concluding in Louisiana in July 2018. Jackson and Gregg—who, among others, reprise their roles from previous MCU films—were digitally&nbsp;<a href=\"https://en.wikipedia.org/wiki/De-aging_in_film\" title=\"De-aging in film\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">de-aged</a>&nbsp;in&nbsp;<a href=\"https://en.wikipedia.org/wiki/Post-production\" title=\"Post-production\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">post-production</a>&nbsp;to reflect the film's 1990s setting.</b></font></p><p style=\"margin: 0.5em 0px; font-size: 14px; background-color: rgb(255, 255, 255);\"><font color=\"#ff8000\" style=\"\" face=\"Comic Sans MS\"><b style=\"\"><i style=\"\">Captain Marvel</i>&nbsp;premiered in London on February 27, 2019, and was theatrically released in the United States on March 8. The film grossed over $1.1&nbsp;billion worldwide, making it the first female-led superhero film to pass the billion-dollar mark. It ranks as the&nbsp;<a href=\"https://en.wikipedia.org/wiki/2019_in_film#Highest-grossing_films\" title=\"2019 in film\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">fourth-highest-grossing film of 2019</a>, and became the&nbsp;<a href=\"https://en.wikipedia.org/wiki/List_of_highest-grossing_superhero_films\" title=\"List of highest-grossing superhero films\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">ninth-highest-grossing superhero film</a>&nbsp;and&nbsp;<a href=\"https://en.wikipedia.org/wiki/List_of_highest-grossing_films#Highest-grossing_films\" title=\"List of highest-grossing films\" style=\"background-image: none; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;\">22nd-highest-grossing film</a>&nbsp;overall. The film received praise for the performances of the cast, particularly that of Larson. A sequel is in development.</b></font></p>",
                    Title = "Captain Marvel",
                    PublishedOn = DateTime.Now,
                    UserId = adminId
                };

                blogList.Add(captainMarvel);

                #endregion

                _dbContext.Blogs.AddRange(blogList);
            }

            #endregion

            // Seed heroes with movies

            #region Heroes

            if (_dbContext.Heroes.Any()) { _dbContext.SaveChangesAsync(); return; }

            var heroList = new List<Hero>();
            var cptAmericaMovies = new List<Movie>();
            var spidermanMovies = new List<Movie>();
            var hulkMovies = new List<Movie>();
            var blackWidowMovies = new List<Movie>();
            var blackPantherMovies = new List<Movie>();
            var thorMovies = new List<Movie>();
            var hawkeyeMovies = new List<Movie>();
            var ironmanMovies = new List<Movie>();
            var antmanMovies = new List<Movie>();
            var venomMovies = new List<Movie>();
            var thanosMovies = new List<Movie>();
            var drstrangeMovies = new List<Movie>();

            #region CaptainAmericaSeeder

            var cptAmerica = new Hero
            {
                Name = "Captain America",
                Description = "Captain America is a fictional superhero appearing in American comic books published by Marvel Comics Created by cartoonists Joe Simon and Jack Kirby, the character first appeared in Captain America Comics #1 (cover datedMarch 1941) from Timely Comics, a predecessor of Marvel Comics. Captain America was designed as a patriotic supersoldierwho often fought the Axis powers of World War II and was Timely Comics' most popular character during the wartime period The popularity of superheroes waned following the war and the Captain America comic book was discontinued in 1950, witha short-lived revival in 1953. Since Marvel Comics revived the character in 1964, Captain America has remained inpublication.",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707816/HeroUploads/cpt_america_img_h3vbyt.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568818599/HeroUploads/cpt-america-cover_mcbtj7.jpg",
                RealName = "Chris Evans",
                Birthday = new DateTime(1981, 6, 13).Date,
                Gender = "Male"
            };

            cptAmericaMovies.Add(new Movie
            {
                Title = "Captain America",
                Hero = cptAmerica
            });

            cptAmericaMovies.Add(new Movie
            {
                Title = "Captain America: The First Avenger",
                Hero = cptAmerica
            });

            cptAmericaMovies.Add(new Movie
            {
                Title = "Captain America: The Winter Soldier",
                Hero = cptAmerica
            });

            cptAmericaMovies.Add(new Movie
            {
                Title = "Captain America: Civil War",
                Hero = cptAmerica
            });

            cptAmerica.Movies.AddRange(cptAmericaMovies);
            heroList.Add(cptAmerica);

            #endregion

            #region SpiderManSeeder

            var spiderman = new Hero
            {
                Name = "Spider-Man",
                Description = "Spider-Man is a fictional superhero created by writer-editor Stan Lee and writer-artist Steve Ditko.He first appeared in the anthology comic book Amazing Fantasy #15 (August 1962) in the Silver Age of Comic Books. Heappears in American comic books published by Marvel Comics, as well as in a number of movies, television shows, and vide game adaptations set in the Marvel Universe. In the stories, Spider-Man is the alias of Peter Parker, an orphan raisedby his Aunt May and Uncle Ben in New York City after his parents Richard and Mary Parker were killed in a plane crash.Lee and Ditko had the character deal with the struggles of adolescence and financial issues, and accompanied him withmany supporting characters, such as J. Jonah Jameson, Harry Osborn, Max Modell, romantic interests Gwen Stacy and MaryJane Watson, and foes such as Doctor Octopus, Green Goblin and Venom. His origin story has him acquiring spider-relatedabilities after a bite from a radioactive spider; these include clinging to surfaces, shooting spider-webs from wristmounted devices, and detecting danger with his \"spider-sense\".",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/spiderman-img_vcfskb.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/spiderman-cover_akceyp.jpg",
                RealName = "Tobey Maguire",
                Birthday = new DateTime(1975, 6, 27).Date,
                Gender = "Male"
            };

            spidermanMovies.Add(new Movie
            {
                Title = "Spider-Man",
                Hero = spiderman
            });

            spidermanMovies.Add(new Movie
            {
                Title = "Spider-Man 2",
                Hero = spiderman
            });

            spidermanMovies.Add(new Movie
            {
                Title = "Spider-Man 3",
                Hero = spiderman
            });

            spidermanMovies.Add(new Movie
            {
                Title = "The Amazing Spider-Man",
                Hero = spiderman
            });

            spidermanMovies.Add(new Movie
            {
                Title = "The Amazing Spider-Man 2",
                Hero = spiderman
            });

            spidermanMovies.Add(new Movie
            {
                Title = "Spider-Man: Homecoming",
                Hero = spiderman
            });

            spidermanMovies.Add(new Movie
            {
                Title = "Spider-Man: Into the Spider-Verse",
                Hero = spiderman
            });

            spidermanMovies.Add(new Movie
            {
                Title = "Spider-Man: Far From Home",
                Hero = spiderman
            });

            spiderman.Movies.AddRange(spidermanMovies);

            heroList.Add(spiderman);

            #endregion

            #region ThorSeeder

            var thor = new Hero
            {
                Name = "Thor",
                Description = "Thor is a 2011 American superhero film based on the Marvel Comics character of the same name, produce by Marvel Studios and distributed by Paramount Pictures.[N 1] It is the fourth film in the Marvel Cinematic Universe(MCU). The film was directed by Kenneth Branagh, written by the writing team of Ashley Edward Miller and Zack Stentzalong with Don Payne, and stars Chris Hemsworth as the title character, alongside Natalie Portman, Tom Hiddleston,Stellan Skarsgård, Colm Feore, Ray Stevenson, Idris Elba, Kat Dennings, Rene Russo, and Anthony Hopkins. The film seesThor banished to Earth from Asgard, stripped of his powers and his hammer Mjölnir, after reigniting a dormant war. As hi brother Loki plots to take the Asgardian throne, Thor must prove himself worthy.",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707791/HeroUploads/thor-img_aeb3pa.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707792/HeroUploads/thor-cover_ab0mdt.jpg",
                RealName = "Chris Hemsworth",
                Birthday = new DateTime(1983, 8, 11).Date,
                Gender = "Male"
            };

            thorMovies.Add(new Movie
            {
                Title = "Thor",
                Hero = thor
            });

            thorMovies.Add(new Movie
            {
                Title = "Thor: Ragnarok",
                Hero = thor
            });

            thorMovies.Add(new Movie
            {
                Title = "Thor: The Dark World",
                Hero = thor
            });

            thorMovies.Add(new Movie
            {
                Title = "Thor: Love and Thunder",
                Hero = thor
            });

            thor.Movies.AddRange(thorMovies);

            heroList.Add(thor);

            #endregion

            #region HulkSeeder

            var hulk = new Hero
            {
                Name = "Hulk",
                Description = "Hulk is a 2003 American superhero film based on the fictional Marvel Comics character of the same nam directed by Ang Lee which stars Eric Bana as the title character, Jennifer Connelly as Betty Ross, Sam Elliott asGeneral Thaddeus E. \"Thunderbolt\" Ross, Josh Lucas, and Nick Nolte as Bruce's father. The film explores the origins ofBruce Banner, who after a lab accident involving gamma radiation finds himself able to turn into a huge green-skinnedmonster whenever he is emotionally provoked or stressed, while he is pursued by the United States military and comes int a conflict with his biological father, who has his own dark agenda for his son.",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707689/HeroUploads/hulk-img_dauwaa.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/hulk-cover_mgxrcq.jpg",
                RealName = "Mark Ruffalo",
                Birthday = new DateTime(1967, 11, 22).Date,
                Gender = "Male"
            };

            hulkMovies.Add(new Movie
            {
                Title = "The Incredible Hulk",
                Hero = hulk
            });

            hulkMovies.Add(new Movie
            {
                Title = "Hulk",
                Hero = hulk
            });

            hulk.Movies.AddRange(hulkMovies);

            heroList.Add(hulk);

            #endregion

            #region IronManSeeder

            var ironman = new Hero
            {
                Name = "Iron Man",
                Description = "Iron Man is a 2008 American superhero film based on the Marvel Comics character of the same name,produced by Marvel Studios and distributed by Paramount Pictures. The first installment of the Marvel Cinematic Universe it was directed by Jon Favreau from a screenplay by Mark Fergus and Hawk Ostby, and Art Marcum and Matt Holloway. Thefilm follows Tony Stark (Robert Downey Jr.), an industrialist and master engineer who builds a mechanized suit of armorand becomes the superhero Iron Man.",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707689/HeroUploads/ironman-img_kvxuvy.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707689/HeroUploads/ironman-cover_kvdzuy.jpg",
                RealName = "Robert Downey Jr",
                Birthday = new DateTime(1965, 4, 4).Date,
                Gender = "Male"
            };

            ironmanMovies.Add(new Movie
            {
                Title = "Iron Man",
                Hero = ironman
            });

            ironmanMovies.Add(new Movie
            {
                Title = "Iron Man 2",
                Hero = ironman
            });

            ironmanMovies.Add(new Movie
            {
                Title = "Iron Man 3",
                Hero = ironman
            });

            ironman.Movies.AddRange(ironmanMovies);

            heroList.Add(ironman);

            #endregion

            #region HawkEyeSeeder

            var hawkeye = new Hero
            {
                Name = "Hawkeye",
                Description = "Hawkeye (Clinton Francis \"Clint\" Barton) is a fictional superhero appearing in American comic bookspublished by Marvel Comics. Created by writer Stan Lee and artist Don Heck, the character first appeared as a villain inTales of Suspense #57 (Sept. 1964) and later joined the Avengers in The Avengers #16 (May 1965). He has been a prominentmember of the team ever since. He was also ranked at #44 on IGN's Top 100 Comic Book Heroes list.[3]",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/hawkeye-img_lvvkth.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568707690/HeroUploads/hawkeye-cover_dpzgsd.jpg",
                RealName = "Jeremy Renner",
                Birthday = new DateTime(1971, 1, 7).Date,
                Gender = "Male"
            };

            hawkeyeMovies.Add(new Movie
            {
                Title = "Avengers",
                Hero = hawkeye
            });

            hawkeyeMovies.Add(new Movie
            {
                Title = "The Avengers",
                Hero = hawkeye
            });

            hawkeye.Movies.AddRange(hawkeyeMovies);

            heroList.Add(hawkeye);

            #endregion

            #region BlackWidowSeeder

            var blackwidow = new Hero
            {
                Name = "Black Widow",
                Description = "Black Widow is an upcoming American superhero film based on the Marvel Comics character of the samename. Produced by Marvel Studios and distributed by Walt Disney Studios Motion Pictures, it is intended to be the twentyfourth film in the Marvel Cinematic Universe (MCU). The film is directed by Cate Shortland, written by Jac Schaeffer andNed Benson, and stars Scarlett Johansson as Natasha Romanoff / Black Widow, alongside David Harbour, Florence Pugh, O-TFagbenle, and Rachel Weisz.",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568712498/HeroUploads/blackwidow-img_kvu6ch.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568712498/HeroUploads/blackwidow-cover_kuhthy.jpg",
                RealName = "Scarlett Johansson",
                Birthday = new DateTime(1984, 11, 22).Date,
                Gender = "Female"
            };

            blackWidowMovies.Add(new Movie
            {
                Title = "Black Widow",
                Hero = blackwidow
            });

            blackwidow.Movies.AddRange(blackWidowMovies);

            heroList.Add(blackwidow);

            #endregion

            #region BlackPantherSeeder

            var blackpanther = new Hero
            {
                Name = "Black Panther",
                Description = "Black Panther is a 2018 American superhero film based on the Marvel Comics character of the same name Produced by Marvel Studios and distributed by Walt Disney Studios Motion Pictures, it is the eighteenth film in theMarvel Cinematic Universe (MCU). The film is directed by Ryan Coogler, who co-wrote the screenplay with Joe Robert Cole,and stars Chadwick Boseman as T'Challa / Black Panther, alongside Michael B. Jordan, Lupita Nyong'o, Danai Gurira, Marti Freeman, Daniel Kaluuya, Letitia Wright, Winston Duke, Angela Bassett, Forest Whitaker, and Andy Serkis. In BlackPanther, T'Challa is crowned king of Wakanda following his father's death, but his sovereignty is challenged by anadversary who plans to abandon the country's isolationist policies and begin a global revolution.",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1568712497/HeroUploads/black-panther-img_ea6z3e.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1568712498/HeroUploads/black-panther-cover_gulfqb.jpg",
                RealName = "Chadwick Boseman",
                Birthday = new DateTime(1976, 11, 29).Date,
                Gender = "Male"
            };

            blackPantherMovies.Add(new Movie
            {
                Title = "Black Panther",
                Hero = blackpanther
            });

            blackpanther.Movies.AddRange(blackPantherMovies);

            heroList.Add(blackpanther);

            #endregion

            #region AntManSeeder

            var antman = new Hero
            {
                Name = "Ant-Man",
                Description = "Ant-Man is a 2015 American superhero film based on the Marvel Comics characters of the same name: Scott Lang and Hank Pym. Produced by Marvel Studios and distributed by Walt Disney Studios Motion Pictures, it is the twelfth film in the Marvel Cinematic Universe (MCU). The film was directed by Peyton Reed, with a screenplay by the writing teams of Edgar Wright and Joe Cornish, and Adam McKay and Paul Rudd. It stars Rudd as Scott Lang / Ant-Man, alongside Evangeline Lilly, Corey Stoll, Bobby Cannavale, Michael Peña, Tip \"T.I.\" Harris, Anthony Mackie, Wood Harris, Judy Greer, David Dastmalchian, and Michael Douglas as Hank Pym. In Ant-Man, Lang must help defend Pym's Ant-Man shrinking technology and plot a heist with worldwide ramifications.",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1571507696/HeroUploads/k7x65bzx6czirkqe7mkf.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1571507697/HeroUploads/csn37cqsynnwvcf8sigj.jpg",
                RealName = "Paul Rudd",
                Birthday = new DateTime(1969, 4, 6).Date,
                Gender = "Male"
            };

            antmanMovies.Add(new Movie
            {
                Title = "Ant-Man",
                Hero = antman
            });

            antmanMovies.Add(new Movie
            {
                Title = "Ant-Man and the Wasp",
                Hero = antman
            });

            antman.Movies.AddRange(antmanMovies);

            heroList.Add(antman);

            #endregion

            #region VenomSeeder

            var venom = new Hero
            {
                Name = "Venom",
                Description = "Venom is a 2018 American superhero film based on the Marvel Comics character of the same name, produced by Columbia Pictures in association with Marvel[5] and Tencent Pictures. Distributed by Sony Pictures Releasing, it is the first film in Sony's Marvel Universe. Directed by Ruben Fleischer from a screenplay by Jeff Pinkner, Scott Rosenberg, and Kelly Marcel, it stars Tom Hardy as Eddie Brock / Venom, alongside Michelle Williams, Riz Ahmed, Scott Haze, and Reid Scott. In Venom, journalist Brock gains superpowers after being bound to an alien symbiote whose species plans to invade Earth.",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1571508451/HeroUploads/zpmgo5isaydrhqndnhvj.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1571508452/HeroUploads/kgyrdi3obeuwygmrwyfv.jpg",
                RealName = "Tom Hardy",
                Birthday = new DateTime(1977, 9, 15).Date,
                Gender = "Male"
            };

            venomMovies.Add(new Movie
            {
                Title = "Venom",
                Hero = venom
            });

            venomMovies.Add(new Movie
            {
                Title = "Spider-Man 3",
                Hero = venom
            });

            venom.Movies.AddRange(venomMovies);

            heroList.Add(venom);

            #endregion

            #region ThanosSeeder

            var thanos = new Hero
            {
                Name = "Thanos",
                Description = "Thanos is a fictional supervillain appearing in American comic books published by Marvel Comics. The character was created by writer-artist Jim Starlin, and made his first appearance in The Invincible Iron Man #55 (cover dated February 1973). Thanos is one of the most powerful villains in the Marvel Universe and has clashed with many heroes including the Avengers, the Guardians of the Galaxy, the Fantastic Four, and the X-Men.\r\n\r\nThe character appeared in the Marvel Cinematic Universe, portrayed by Damion Poitier in The Avengers (2012) and by Josh Brolin in Guardians of the Galaxy (2014), Avengers: Age of Ultron (2015), Avengers: Infinity War (2018), and Avengers: Endgame (2019) through voice and motion capture. The character has also appeared in various comic adaptations, including animated television series and video games.",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1576663479/HeroUploads/thanos_profile_j8jrab.png",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1576663474/HeroUploads/thanos_cover_ozpf4p.jpg",
                RealName = "Thanos",
                Birthday = new DateTime(1973, 2, 1).Date,
                Gender = "Male"
            };

            thanosMovies.Add(new Movie
            {
                Title = "Avengers: Infinity War",
                Hero = thanos
            });

            thanosMovies.Add(new Movie
            {
                Title = "Avengers: Endgame",
                Hero = thanos
            });

            thanos.Movies.AddRange(thanosMovies);

            heroList.Add(thanos);

            #endregion

            #region DoctorStrangeSeeder

            var drstrange = new Hero
            {
                Name = "Doctor Strange",
                Description = "Doctor Strange is a 2016 American superhero film based on the Marvel Comics character of the same name. Produced by Marvel Studios and distributed by Walt Disney Studios Motion Pictures, it is the fourteenth film in the Marvel Cinematic Universe (MCU). The film was directed by Scott Derrickson from a screenplay he wrote with Jon Spaihts and C. Robert Cargill, and stars Benedict Cumberbatch as surgeon Stephen Strange along with Chiwetel Ejiofor, Rachel McAdams, Benedict Wong, Michael Stuhlbarg, Benjamin Bratt, Scott Adkins, Mads Mikkelsen, and Tilda Swinton. In the film, Strange learns the mystic arts after a career-ending car crash.",
                Image = "https://res.cloudinary.com/goofy5752/image/upload/v1576664136/HeroUploads/drstrange_profile_araltf.jpg",
                CoverImage = "https://res.cloudinary.com/goofy5752/image/upload/v1576664135/HeroUploads/drstrange_cover_yfxqzx.jpg",
                RealName = "Benedict Cumberbatch",
                Birthday = new DateTime(1973, 2, 1).Date,
                Gender = "Male"
            };

            drstrangeMovies.Add(new Movie
            {
                Title = "Doctor Strange",
                Hero = thanos
            });

            drstrange.Movies.AddRange(drstrangeMovies);

            heroList.Add(drstrange);

            #endregion

            _dbContext.Heroes.AddRange(heroList);
            _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}