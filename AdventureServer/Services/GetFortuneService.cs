using AdventureServer.Interfaces;
using AdventureServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventureServer.Services
{
    public class GetFortuneService : IGetFortune
    {
        public Fortune errorFortune = new() { id = 999, phrase = "Dark cloud rain on person who try to break fortune API!" };
   
        public Fortune ReturnRandomFortune()
        {
            var _fortunes = InitFortunes();

            Random Rnum = new Random();

            var value = _fortunes.FirstOrDefault(x => x.id == Rnum.Next(0, _fortunes.Count));

            if (value == null)
            {
                return errorFortune;
            }

            return value;
            
        }

        public Fortune ReturnTimeBasedFortune()
        {
            var _fortunes = InitFortunes();

            int second = DateTime.Now.Second;

            var value = _fortunes.FirstOrDefault(x => x.id == second);

            if (value == null)
            {
                return errorFortune;
            }

            return value;

        }

        public Fortune ReturnFortuneById(int id)
        {
            var _fortunes = InitFortunes();

            if (id > _fortunes.Count) return errorFortune;
            
            var value = _fortunes.FirstOrDefault(x => x.id == id);

            if (value == null) return errorFortune;

            return value;
        }

        private List<Fortune> InitFortunes()
        {
            return new List<Fortune>
            {
                // One Fortune for every second in a minute 0 to 59
                new Fortune { id = 0, phrase = "Steve S was here. Stole API fortune." },
                new Fortune { id = 1, phrase = "Some days you are pigeon, some days you are statue. Today, bring you API."},
                new Fortune { id = 2, phrase = "If a API doesn’t have a server, is it naked or homeless?" },
                new Fortune { id = 3, phrase = "You think it’s a secret, but API knows all." },
                new Fortune { id = 4, phrase = "Hard API work pays off in the future. Laziness pays off now." },
                new Fortune { id = 5, phrase = "Ask your mom instead of an API" },
                new Fortune { id = 6, phrase = "Avoid taking unnecessary gambles. Lucky API numbers: 12, 15, 23, 28, 37" },
                new Fortune { id = 7, phrase = "You have error in your API code." },
                new Fortune { id = 8, phrase = "May your API someday be carbon neutral" },
                new Fortune { id = 9, phrase = "You are not illiterate. You are API deprived!" },
                new Fortune { id = 10, phrase = "Don’t API let statistics do a number on you" },
                new Fortune { id = 11, phrase = "Some fortune APIs contain no fortune" },
                new Fortune { id = 12, phrase = "You will receive a fortune API result." },
                new Fortune { id = 13, phrase = "No API snowflake feels responsible in an avalanche." },
                new Fortune { id = 14, phrase = "You love fortune API but API think you only as friend." },
                new Fortune { id = 15, phrase = "There is no mistake so great as that of getting bad message from API." },
                new Fortune { id = 16, phrase = "That wasn’t chicken it was API call." },
                new Fortune { id = 17, phrase = "Someone will invite you to a API Karaoke party." },
                new Fortune { id = 18, phrase = "All fortunes are wrong except from this API." },
                new Fortune { id = 19, phrase = "It is a good day to call your mother API." },
                new Fortune { id = 20, phrase = "Only listen to the fortune API; disregard all other fortune telling systems" },
                new Fortune { id = 21, phrase = "Never wear your best pants when you go to fight for freedom. Call API and stay inside!" },
                new Fortune { id = 22, phrase = "Never forget a friend. Especially if he owes you API." },
                new Fortune { id = 23, phrase = "Help! I am being held prisoner in a fortune API factory." },
                new Fortune { id = 24, phrase = "Fortune not found? API was sleepy." },
                new Fortune { id = 25, phrase = "Because of your melodic nature, the API reprts moonlight never misses an appointment." },
                new Fortune { id = 26, phrase = "You can always find happiness at work on Friday by calling API for saturday date." },
                new Fortune { id = 27, phrase = "The road to riches is paved with API developers." },
                new Fortune { id = 28, phrase = "The world may be your oyster, but it doesn't mean you'll get its API." },
                new Fortune { id = 29, phrase = "We don’t know the future, but here’s an API." },
                new Fortune { id = 30, phrase = "Some men dream of fortunes, others dream of APIs." },
                new Fortune { id = 31, phrase = "He who throws dirt is losing ground. He who calls API finds answers!" },
                new Fortune { id = 32, phrase = "He who laughs at himself never runs out API calles finds things to laugh at." },
                new Fortune { id = 33, phrase = "Flattery will go far tonight except API thinks you need to take bath." },
                new Fortune { id = 34, phrase = "Fortuen API remineds you do not mistake temptation for opportunity. " },
                new Fortune { id = 35, phrase = "An alien API of some sort will be appearing to you shortly." },
                new Fortune { id = 36, phrase = "You will live long enough to call many fortune APIs." },
                new Fortune { id = 37, phrase = "If you look back and other APIs, you’ll soon be going that way." },
                new Fortune { id = 38, phrase = "Person who calls API and then rests on laurels gets thorn in backside." },
                new Fortune { id = 39, phrase = "He who dies with most toys, still dies but API calls always return 200." },
                new Fortune { id = 40, phrase = "Probability of API being seen directly proportional to stupidity of act seems impossible except for you." },
                new Fortune { id = 41, phrase = "You will soon have have out of API and body experience" },
                new Fortune { id = 42, phrase = "Wise person need either good manners, fast reflexes or better API." },
                new Fortune { id = 43, phrase = "Okay to look at past and future. Just don’t stare at the API." },
                new Fortune { id = 44, phrase = "Person who call fortune api get best fortune." },
                new Fortune { id = 45, phrase = "You are cleverly disguised as responsible adult who called an API." },
                new Fortune { id = 46, phrase = "When chosen for jury duty, tell judge fortune API say guilty!" },
                new Fortune { id = 47, phrase = "Your inferiority complex not good enough. Try harder and call API before you visit!" },
                new Fortune { id = 48, phrase = "Two days from now, tomorrow will be yesterday but API will be here for you today." },
                new Fortune { id = 49, phrase = "Wise person never try to get even. Wiser person get odder. Wisest person call API daily!" },
                new Fortune { id = 50, phrase = "The fortune you seek WAS here but left to visit mother API." },
                new Fortune { id = 51, phrase = "A closed mouth gathers no feet. A closed API gathers no traffic." },
                new Fortune { id = 52, phrase = "An uncalled API provides no result." },
                new Fortune { id = 53, phrase = "A conclusion is simply the place where you got tired of thinking dur to lack of calling API." },
                new Fortune { id = 54, phrase = "A cynic is only a frustrated optimist that never called this fortune API." },
                new Fortune { id = 55, phrase = "Your reality check about to bounced but this API catch you." },
                new Fortune { id = 56, phrase = "A foolish man listens to his heart. A wise man listens to APIs." },
                new Fortune { id = 57, phrase = "A fanatic is one who can't change his mind, and won't change the subject of the API call." },
                new Fortune { id = 58, phrase = "API says Magic 8 ball is fake. Try again later. " },
                new Fortune { id = 59, phrase = "Time is up, you need to make that choice soon. Call API later for same result." }

             };

        }

    }
}



