using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Globalization;
using TBGC.Models;
using Microsoft.AspNetCore.Identity;
using Models;
using System.IO;
using System.Reflection.Metadata;

namespace TBGC.DataAccess.Data
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser>

    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> Options) : base(Options)
        {
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<GolfCourse> GolfCourses { get; set; }
        public DbSet<GolfCourseHole> GolfCourseHoles { get; set; }
        public DbSet<GolfRound> GolfRounds { get; set; }
        public DbSet<GolfGroup> GolfGroups { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<GolfPlayer> GolfPlayers { get; set; }
        public DbSet<GolfPlayerScore> GolfPlayerScores { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>().HasData(
            new Member
            { MemberId = 1, Email = "willffdunn@gmail.com", FirstName = "William",
                LastName = "Dunn", MemberStatus = "Active", 
                EmailConfirmed = false, MemberType = "TBRes", Handicap = 9,
                PhoneNumber = "484-885-7000",
                MemberTee = "White",
                PreferredNotification = "Both",
            },
            new Member
            { MemberId = 2, Email = "karenfdunn@gmail.com", FirstName = "Karen",
                LastName = "Dunn", MemberStatus = "Active", 
                EmailConfirmed = false, MemberType = "TBRes", Handicap = 19,
                PhoneNumber = "610-733-38380",
                MemberTee = "Red",
                PreferredNotification = "Both",
            });
            modelBuilder.Entity<GolfRound>().HasData(
            new GolfRound
            {
                GRId = 1,
                GCId =1,
                GRDate = DateTime.Parse("2023-07-01 00:00:00"),
                GRType = "TBGC Club, Stroke Play"

             });
            modelBuilder.Entity<GolfGroup>().HasData(
            new GolfGroup
            {
                GGId = 1,
                GRId = 1,
                GGTtime = TimeSpan.Parse("06:54:00"),
                GGName = "Group 1"

            });
            modelBuilder.Entity<GolfPlayer>().HasData(
            new GolfPlayer
            {
                GGId = 1,
                GPId = 1,
                MemberId = 1,
                Tot1 = 0,
                Tot2 = 0,
                Tot3 = 0,
                PTot1 = 0,
                PTot2 = 0,
                PTot3 = 0,
                GTot1 = 0,
                GTot2 = 0,
                GTot3 = 0,
                FTot1 = 0,
                FTot2 = 0,
                FTot3 = 0,
                GPType = "Member",
                GPFName = "William",
                GPLName = "Dunn"


            });
            modelBuilder.Entity<GolfPlayerScore>().HasData(
            new GolfPlayerScore
            {
                GPSId = 1,
                GPId = 1,
                GHId = 1,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 2,
                GPId = 1,
                GHId = 2,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 3,
                GPId = 1,
                GHId = 3,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 4,
                GPId = 1,
                GHId = 4,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
             new GolfPlayerScore
             {
                 GPSId = 5,
                 GPId = 1,
                 GHId = 5,
                 GPSStrokes = 4,
                 GPSPutts = 2,
                 GPSGIY = "Yes",
                 GPSFW = "Yes"
             },
            new GolfPlayerScore
            {
                GPSId = 6,
                GPId = 1,
                GHId = 6,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 7,
                GPId = 1,
                GHId = 7,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 8,
                GPId = 1,
                GHId = 8,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 9,
                GPId = 1,
                GHId = 9,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 10,
                GPId = 1,
                GHId = 10,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 11,
                GPId = 1,
                GHId = 11,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 12,
                GPId = 1,
                GHId = 12,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 13,
                GPId = 1,
                GHId = 13,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 14,
                GPId = 1,
                GHId = 14,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 15,
                GPId = 1,
                GHId = 15,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 16,
                GPId = 1,
                GHId = 16,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 17,
                GPId = 1,
                GHId = 17,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            },
            new GolfPlayerScore
            {
                GPSId = 18,
                GPId = 1,
                GHId = 18,
                GPSStrokes = 4,
                GPSPutts = 2,
                GPSGIY = "Yes",
                GPSFW = "Yes"
            });
            modelBuilder.Entity<Event>().HasData(
            new Event
            {
                EvId = 1,
                EvShortDescription = "Event Title",
                EvDescription = "First Event",
                EvType = "Golf Round",
                EvDate = DateTime.Parse("2023-07-01 00:00:00")

             });

        modelBuilder.Entity<GolfCourse>().HasData(
            new GolfCourse
            {
                GCId = 1,
                GCName = "Palm Beach National Golf & Country Club",
                GCStreet = "7500 St Andrews Rd",
                GCCity = "Lake Worth",
                GCState = "Fl",
                GCZip = "12345",
                GCType = "Public",
                GCPhoneNumber = "(561) 965-3381",
                GCComments = "Comment number 1",
                GCNbrHoles = 18,
                GCName1 = "",
                GCName2 = "",
                GCName3 = "",
                GCT1 = "Black",
                GCT2 = "Blue",
                GCT3 = "White",
                GCT4 = "Gold",
                GCT5 = "Red",
                GCC1 = "White",
                GCC2 = "White",
                GCC3 = "Black",
                GCC4 = "Black",
                GCC5 = "White",
                GCT1TotIn = 3368,
                GCT1TotOut = 3366,
                GCT1Tot = 6734,
                GCT2TotIn = 3287,
                GCT2TotOut = 3285,
                GCT2Tot = 6572,
                GCT3TotIn = 3072,
                GCT3TotOut = 3066,
                GCT3Tot = 6138,
                GCT4TotIn = 2842,
                GCT4TotOut = 2837,
                GCT4Tot = 5679,
                GCT5TotIn = 2638,
                GCT5TotOut = 2448,
                GCT5Tot = 5086,
                GCParIn = 36,
                GCParOut = 36,
                GCParTot = 72


    },
            new GolfCourse
            {
                GCId = 3,
                GCName = "Jamestown Golf Course",
                GCStreet = "245 Conanicus Avenue",
                GCCity = "Jamestown",
                GCState = "RI",
                GCZip = "02835",
                GCType = "Public",
                GCPhoneNumber = "(401) 423-9930",
                GCComments = "Comment number 2",
                GCNbrHoles = 9,
                GCName1 = "",
                GCName2 = "",
                GCName3 = "",
                GCT1 = "Blue",
                GCT2 = "White",
                GCT3 = "Red",
                GCT4 = "White",
                GCT5 = "White",
                GCC1 = "White",
                GCC2 = "Black",
                GCC3 = "Black",
                GCC4 = "White",
                GCC5 = "White",
                GCT1TotIn = 0,
                GCT1TotOut = 0,
                GCT1Tot = 0,
                GCT2TotIn = 0,
                GCT2TotOut = 0,
                GCT2Tot = 0,
                GCT3TotIn = 0,
                GCT3TotOut = 0,
                GCT3Tot = 0,
                GCT4TotIn = 0,
                GCT4TotOut = 0,
                GCT4Tot = 0,
                GCT5TotIn = 0,
                GCT5TotOut = 0,
                GCT5Tot = 0,
                GCParIn = 36,
                GCParOut = 36,
                GCParTot = 72
            },

            new GolfCourse
            {
                GCId = 2,
                GCName = "The Links at Boyton Beach",
                GCStreet = "8020 Jog Rd",
                GCCity = "Boyton Beach",
                GCState = "Fl",
                GCZip = "12345",
                GCType = "Public",
                GCPhoneNumber = "(561) 742-6500",
                GCComments = "Comment number 2",
                GCNbrHoles = 18,
                GCName1 = "",
                GCName2 = "",
                GCName3 = "",
                GCT1 = "Gold",
                GCT2 = "Blue",
                GCT3 = "White",
                GCT4 = "Orange",
                GCT5 = "Red",
                GCC1 = "White",
                GCC2 = "White",
                GCC3 = "Black",
                GCC4 = "Black",
                GCC5 = "White",
                GCT1TotIn = 0,
                GCT1TotOut = 0,
                GCT1Tot = 0,
                GCT2TotIn = 0,
                GCT2TotOut = 0,
                GCT2Tot = 0,
                GCT3TotIn = 0,
                GCT3TotOut = 0,
                GCT3Tot = 0,
                GCT4TotIn = 0,
                GCT4TotOut = 0,
                GCT4Tot = 0,
                GCT5TotIn = 0,
                GCT5TotOut = 0,
                GCT5Tot = 0,
                GCParIn = 36,
                GCParOut = 36,
                GCParTot = 72
            },

            new GolfCourse
            {
                GCId = 4,
                GCName = "Green Valley Country Club",
                GCStreet = "371 Union St",
                GCCity = "Portsmouth",
                GCState = "RI",
                GCZip = "02871",
                GCType = "Public",
                GCPhoneNumber = "(401)847-9543",
                GCComments = "Comment number 2",
                GCNbrHoles = 18,
                GCName1 = "",
                GCName2 = "",
                GCName3 = "",
                GCT1 = "Blue",
                GCT2 = "White",
                GCT3 = "Yellow",
                GCT4 = "Red",
                GCT5 = "White",
                GCC1 = "White",
                GCC2 = "Black",
                GCC3 = "Black",
                GCC4 = "Black",
                GCC5 = "White",
                GCT1TotIn = 0,
                GCT1TotOut = 0,
                GCT1Tot = 0,
                GCT2TotIn = 0,
                GCT2TotOut = 0,
                GCT2Tot = 0,
                GCT3TotIn = 0,
                GCT3TotOut = 0,
                GCT3Tot = 0,
                GCT4TotIn = 0,
                GCT4TotOut = 0,
                GCT4Tot = 0,
                GCT5TotIn = 0,
                GCT5TotOut = 0,
                GCT5Tot = 0,
                GCParIn = 36,
                GCParOut = 36,
                GCParTot = 72
            });
            modelBuilder.Entity<GolfCourseHole>().HasData(
              
               new GolfCourseHole
               {
                   GCName = "Jamestown Golf Course",
                   GCId = 3,
                   GHId = 37,
                   GHSectName = "Front",
                   GHHandicap = 1,
                   GHHole = 1,
                   GHPar = 4,
                   GHT1 = 300,
                   GHT2 = 280,
                   GHT3 = 248,
                   GHT4 = 0,
                   GHT5 = 0
               },
            new GolfCourseHole
            {
                GCName = "Jamestown Golf Course",
                GCId = 3,
                GHId = 38,
                GHSectName = "Front",
                GHHandicap = 3,
                GHHole = 2,
                GHPar = 5,
                GHT1 = 541,
                GHT2 = 484,
                GHT3 = 426,
                GHT4 = 0,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "Jamestown Golf Course",
                GCId = 3,
                GHId = 39,
                GHSectName = "Front",
                GHHandicap = 17,
                GHHole = 3,
                GHPar = 4,
                GHT1 = 293,
                GHT2 = 279,
                GHT3 = 240,
                GHT4 = 0,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "Jamestown Golf Course",
                GCId = 3,
                GHId = 40,
                GHSectName = "Front",
                GHHandicap = 5,
                GHHole = 4,
                GHPar = 4,
                GHT1 = 403,
                GHT2 = 375,
                GHT3 = 317,
                GHT4 = 0,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "Jamestown Golf Course",
                GCId = 3,
                GHId = 41,
                GHSectName = "Front",
                GHHandicap = 7,
                GHHole = 5,
                GHPar = 3,
                GHT1 = 130,
                GHT2 = 114,
                GHT3 = 100,
                GHT4 = 0,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "Jamestown Golf Course",
                GCId = 3,
                GHId = 42,
                GHSectName = "Front",
                GHHandicap = 11,
                GHHole = 6,
                GHPar = 5,
                GHT1 = 450,
                GHT2 = 379,
                GHT3 = 351,
                GHT4 = 0,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "Jamestown Golf Course",
                GCId = 3,
                GHId = 43,
                GHSectName = "Front",
                GHHandicap = 15,
                GHHole = 7,
                GHPar = 3,
                GHT1 = 161,
                GHT2 = 141,
                GHT3 = 130,
                GHT4 = 0,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "Jamestown Golf Course",
                GCId = 3,
                GHId = 44,
                GHSectName = "Front",
                GHHandicap = 13,
                GHHole = 8,
                GHPar = 4,
                GHT1 = 405,
                GHT2 = 368,
                GHT3 = 327,
                GHT4 = 0,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "Jamestown Golf Course",
                GCId = 3,
                GHId = 45,
                GHSectName = "Front",
                GHHandicap = 9,
                GHHole = 9,
                GHPar = 4,
                GHT1 = 375,
                GHT2 = 328,
                GHT3 = 292,
                GHT4 = 0,
                GHT5 = 0
            });
            modelBuilder.Entity<GolfCourseHole>().HasData(

             new GolfCourseHole
             {
                 GCName = "The Links at Boyton Beach",
                 GCId = 2,
                 GHId = 65,
                 GHSectName = "Front",
                 GHHandicap = 4,
                 GHHole = 1,
                 GHPar = 5,
                 GHT1 = 509,
                 GHT2 = 500,
                 GHT3 = 451,
                 GHT4 = 439,
                 GHT5 = 0
             },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 66,
                GHSectName = "Front",
                GHHandicap = 18,
                GHHole = 2,
                GHPar = 3,
                GHT1 = 156,
                GHT2 = 140,
                GHT3 = 139,
                GHT4 = 123,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 67,
                GHSectName = "Front",
                GHHandicap = 2,
                GHHole = 3,
                GHPar = 4,
                GHT1 = 431,
                GHT2 = 399,
                GHT3 = 388,
                GHT4 = 373,
                GHT5 = 0
            },
             new GolfCourseHole
             {
                 GCName = "The Links at Boyton Beach",
                 GCId = 2,
                 GHId = 68,
                 GHSectName = "Front",
                 GHHandicap = 8,
                 GHHole = 4,
                 GHPar = 4,
                 GHT1 = 420,
                 GHT2 = 389,
                 GHT3 = 330,
                 GHT4 = 302,
                 GHT5 = 0
             },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 69,
                GHSectName = "Front",
                GHHandicap = 10,
                GHHole = 5,
                GHPar = 4,
                GHT1 = 388,
                GHT2 = 338,
                GHT3 = 255,
                GHT4 = 250,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 70,
                GHSectName = "Front",
                GHHandicap = 12,
                GHHole = 6,
                GHPar = 4,
                GHT1 = 358,
                GHT2 = 340,
                GHT3 = 266,
                GHT4 = 240,
                GHT5 = 0
            },

            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 71,
                GHSectName = "Front",
                GHHandicap = 16,
                GHHole = 7,
                GHPar = 3,
                GHT1 = 186,
                GHT2 = 145,
                GHT3 = 86,
                GHT4 = 80,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 72,
                GHSectName = "Front",
                GHHandicap = 6,
                GHHole = 8,
                GHPar = 4,
                GHT1 = 307,
                GHT2 = 298,
                GHT3 = 258,
                GHT4 = 250,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 73,
                GHSectName = "Front",
                GHHandicap = 14,
                GHHole = 9,
                GHPar = 4,
                GHT1 = 324,
                GHT2 = 313,
                GHT3 = 296,
                GHT4 = 289,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 74,
                GHSectName = "Front",
                GHHandicap = 11,
                GHHole = 10,
                GHPar = 4,
                GHT1 = 368,
                GHT2 = 340,
                GHT3 = 326,
                GHT4 = 294,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 75,
                GHSectName = "Front",
                GHHandicap = 1,
                GHHole = 11,
                GHPar = 5,
                GHT1 = 515,
                GHT2 = 478,
                GHT3 = 455,
                GHT4 = 391,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 76,
                GHSectName = "Front",
                GHHandicap = 3,
                GHHole = 12,
                GHPar = 4,
                GHT1 = 395,
                GHT2 = 374,
                GHT3 = 308,
                GHT4 = 302,
                GHT5 = 0
            }, new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 77,
                GHSectName = "Front",
                GHHandicap = 13,
                GHHole = 13,
                GHPar = 4,
                GHT1 = 353,
                GHT2 = 336,
                GHT3 = 321,
                GHT4 = 316,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 78,
                GHSectName = "Front",
                GHHandicap = 9,
                GHHole = 14,
                GHPar = 4,
                GHT1 = 329,
                GHT2 = 309,
                GHT3 = 226,
                GHT4 = 216,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 79,
                GHSectName = "Front",
                GHHandicap = 15,
                GHHole = 15,
                GHPar = 3,
                GHT1 = 179,
                GHT2 = 159,
                GHT3 = 102,
                GHT4 = 96,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 80,
                GHSectName = "Front",
                GHHandicap = 5,
                GHHole = 16,
                GHPar = 4,
                GHT1 = 386,
                GHT2 = 369,
                GHT3 = 300,
                GHT4 = 292,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 81,
                GHSectName = "Front",
                GHHandicap = 17,
                GHHole = 17,
                GHPar = 3,
                GHT1 = 178,
                GHT2 = 158,
                GHT3 = 127,
                GHT4 = 80,
                GHT5 = 0
            },
            new GolfCourseHole
            {
                GCName = "The Links at Boyton Beach",
                GCId = 2,
                GHId = 82,
                GHSectName = "Front",
                GHHandicap = 7,
                GHHole = 18,
                GHPar = 5,
                GHT1 = 515,
                GHT2 = 493,
                GHT3 = 475,
                GHT4 = 398,
                GHT5 = 0
               });
            modelBuilder.Entity<GolfCourseHole>().HasData(
       new GolfCourseHole
       {
           GCName = "Green Valley Country Club",
           GCId = 4,
           GHId = 46,
           GHSectName = "Front",
           GHHandicap = 7,
           GHHole = 1,
           GHPar = 4,
           GHT1 = 370,
           GHT2 = 361,
           GHT3 = 345,
           GHT4 = 331,
           GHT5 = 0
       },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 47,
       GHSectName = "Front",
       GHHandicap = 1,
       GHHole = 2,
       GHPar = 4,
       GHT1 = 461,
       GHT2 = 454,
       GHT3 = 420,
       GHT4 = 341,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 48,
       GHSectName = "Front",
       GHHandicap = 13,
       GHHole = 3,
       GHPar = 4,
       GHT1 = 397,
       GHT2 = 386,
       GHT3 = 355,
       GHT4 = 329,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 49,
       GHSectName = "Front",
       GHHandicap = 3,
       GHHole = 4,
       GHPar = 5,
       GHT1 = 550,
       GHT2 = 541,
       GHT3 = 514,
       GHT4 = 474,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 50,
       GHSectName = "Front",
       GHHandicap = 11,
       GHHole = 5,
       GHPar = 3,
       GHT1 = 190,
       GHT2 = 175,
       GHT3 = 132,
       GHT4 = 125,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 51,
       GHSectName = "Front",
       GHHandicap = 9,
       GHHole = 6,
       GHPar = 4,
       GHT1 = 404,
       GHT2 = 392,
       GHT3 = 358,
       GHT4 = 354,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 52,
       GHSectName = "Front",
       GHHandicap = 17,
       GHHole = 7,
       GHPar = 4,
       GHT1 = 360,
       GHT2 = 354,
       GHT3 = 335,
       GHT4 = 314,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 53,
       GHSectName = "Front",
       GHHandicap = 15,
       GHHole = 8,
       GHPar = 3,
       GHT1 = 210,
       GHT2 = 201,
       GHT3 = 155,
       GHT4 = 145,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 54,
       GHSectName = "Front",
       GHHandicap = 5,
       GHHole = 9,
       GHPar = 4,
       GHT1 = 433,
       GHT2 = 424,
       GHT3 = 375,
       GHT4 = 361,
       GHT5 = 0
   },
    new GolfCourseHole
    {
        GCName = "Green Valley Country Club",
        GCId = 4,
        GHId = 55,
        GHSectName = "Back",
        GHHandicap = 2,
        GHHole = 10,
        GHPar = 5,
        GHT1 = 613,
        GHT2 = 605,
        GHT3 = 576,
        GHT4 = 466,
        GHT5 = 0
    },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 56,
       GHSectName = "Back",
       GHHandicap = 16,
       GHHole = 11,
       GHPar = 3,
       GHT1 = 229,
       GHT2 = 220,
       GHT3 = 187,
       GHT4 = 134,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 57,
       GHSectName = "Back",
       GHHandicap = 18,
       GHHole = 12,
       GHPar = 3,
       GHT1 = 147,
       GHT2 = 125,
       GHT3 = 107,
       GHT4 = 101,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 58,
       GHSectName = "Back",
       GHHandicap = 10,
       GHHole = 13,
       GHPar = 4,
       GHT1 = 342,
       GHT2 = 327,
       GHT3 = 304,
       GHT4 = 252,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 59,
       GHSectName = "Back",
       GHHandicap = 6,
       GHHole = 14,
       GHPar = 4,
       GHT1 = 451,
       GHT2 = 440,
       GHT3 = 414,
       GHT4 = 330,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 60,
       GHSectName = "Back",
       GHHandicap = 9,
       GHHole = 15,
       GHPar = 4,
       GHT1 = 340,
       GHT2 = 334,
       GHT3 = 304,
       GHT4 = 290,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 61,
       GHSectName = "Back",
       GHHandicap = 4,
       GHHole = 16,
       GHPar = 4,
       GHT1 = 407,
       GHT2 = 394,
       GHT3 = 360,
       GHT4 = 336,
       GHT5 = 0
   },
   new GolfCourseHole
   {
       GCName = "Green Valley Country Club",
       GCId = 4,
       GHId = 63,
       GHSectName = "Back",
       GHHandicap = 14,
       GHHole = 17,
       GHPar = 5,
       GHT1 = 548,
       GHT2 = 540,
       GHT3 = 515,
       GHT4 = 449,
       GHT5 = 0
   },
new GolfCourseHole
{
    GCName = "Green Valley Country Club",
    GCId = 4,
    GHId = 64,
    GHSectName = "Back",
    GHHandicap = 8,
    GHHole = 18,
    GHPar = 4,
    GHT1 = 378,
    GHT2 = 368,
    GHT3 = 335,
    GHT4 = 313,
    GHT5 = 0
});
            modelBuilder.Entity<GolfCourseHole>().HasData(
            new GolfCourseHole
            {
                GCName = "Palm Beach National Golf & Country Club",
                GCId = 1,
                GHId = 1,
                GHSectName = "Front",
                GHHandicap = 1,
                GHHole = 1,
                GHPar = 5,
                GHT1 = 509,
                GHT2 = 504,
                GHT3 = 483,
                GHT4 = 405,
                GHT5 = 393
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach National Golf & Country Club",
                GCId = 1,
                GHId = 2,
                GHSectName = "Front",
                GHHandicap = 3,
                GHHole = 2,
                GHPar = 4,
                GHT1 = 390,
                GHT2 = 378,
                GHT3 = 349,
                GHT4 = 327,
                GHT5 = 290
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 3,
                GHSectName = "Front",
                GHHandicap = 17,
                GHHole = 3,
                GHPar = 3,
                GHT1 = 165,
                GHT2 = 158,
                GHT3 = 141,
                GHT4 = 130,
                GHT5 = 93
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 4,
                GHSectName = "Front",
                GHHandicap = 5,
                GHHole = 4,
                GHPar = 4,
                GHT1 = 407,
                GHT2 = 395,
                GHT3 = 352,
                GHT4 = 317,
                GHT5 = 272
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 5,
                GHSectName = "Front",
                GHHandicap = 7,
                GHHole = 5,
                GHPar = 4,
                GHT1 = 426,
                GHT2 = 411,
                GHT3 = 384,
                GHT4 = 369,
                GHT5 = 300
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 6,
                GHSectName = "Front",
                GHHandicap = 11,
                GHHole = 6,
                GHPar = 4,
                GHT1 = 414,
                GHT2 = 412,
                GHT3 = 398,
                GHT4 = 383,
                GHT5 = 277
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 7,
                GHSectName = "Front",
                GHHandicap = 15,
                GHHole = 7,
                GHPar = 5,
                GHT1 = 505,
                GHT2 = 492,
                GHT3 = 465,
                GHT4 = 452,
                GHT5 = 425
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 8,
                GHSectName = "Front",
                GHHandicap = 13,
                GHHole = 8,
                GHPar = 3,
                GHT1 = 179,
                GHT2 = 174,
                GHT3 = 155,
                GHT4 = 143,
                GHT5 = 119
            },
                new GolfCourseHole
                {
                    GCName = "Palm Beach Natioanl Golf & Country Club",
                    GCId = 1,
                    GHId = 9,
                    GHSectName = "Front",
                    GHHandicap = 9,
                    GHHole = 9,
                    GHPar = 4,
                    GHT1 = 371,
                    GHT2 = 361,
                    GHT3 = 339,
                    GHT4 = 311,
                    GHT5 = 279
                },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 10,
                GHSectName = "Back",
                GHHandicap = 10,
                GHHole = 10,
                GHPar = 4,
                GHT1 = 418,
                GHT2 = 404,
                GHT3 = 370,
                GHT4 = 344,
                GHT5 = 318
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 11,
                GHSectName = "Back",
                GHHandicap = 16,
                GHHole = 11,
                GHPar = 3,
                GHT1 = 160,
                GHT2 = 154,
                GHT3 = 145,
                GHT4 = 135,
                GHT5 = 130
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 12,
                GHSectName = "Back",
                GHHandicap = 12,
                GHHole = 12,
                GHPar = 4,
                GHT1 = 360,
                GHT2 = 361,
                GHT3 = 348,
                GHT4 = 324,
                GHT5 = 299
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 13,
                GHSectName = "Back",
                GHHandicap = 2,
                GHHole = 13,
                GHPar = 4,
                GHT1 = 420,
                GHT2 = 413,
                GHT3 = 385,
                GHT4 = 345,
                GHT5 = 329
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 14,
                GHSectName = "Back",
                GHHandicap = 14,
                GHHole = 14,
                GHPar = 5,
                GHT1 = 509,
                GHT2 = 501,
                GHT3 = 472,
                GHT4 = 415,
                GHT5 = 400
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 15,
                GHSectName = "Back",
                GHHandicap = 8,
                GHHole = 15,
                GHPar = 4,
                GHT1 = 390,
                GHT2 = 378,
                GHT3 = 356,
                GHT4 = 333,
                GHT5 = 318
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 16,
                GHSectName = "Back",
                GHHandicap = 18,
                GHHole = 16,
                GHPar = 4,
                GHT1 = 397,
                GHT2 = 384,
                GHT3 = 353,
                GHT4 = 329,
                GHT5 = 262
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 17,
                GHSectName = "Back",
                GHHandicap = 4,
                GHHole = 17,
                GHPar = 4,
                GHT1 = 356,
                GHT2 = 349,
                GHT3 = 332,
                GHT4 = 318,
                GHT5 = 302
            },
            new GolfCourseHole
            {
                GCName = "Palm Beach Natioanl Golf & Country Club",
                GCId = 1,
                GHId = 18,
                GHSectName = "Back",
                GHHandicap = 6,
                GHHole = 18,
                GHPar = 4,
                GHT1 = 358,
                GHT2 = 343,
                GHT3 = 311,
                GHT4 = 299,
                GHT5 = 280
            });
        }        
    }
}


