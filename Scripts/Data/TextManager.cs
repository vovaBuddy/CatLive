using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class TextManager
{
    static private Dictionary<string, string> pets;
    static private Dictionary<string, string> texts;
    static private Dictionary<string, string> dialogs;
    static private Dictionary<string, string> task_names;
    static private Dictionary<string, string> other_strings;

    static private int cur_task_index = 1;
    static private int cur_dialog_index = 1;
    static private void AddDialog(int task_index, string text)
    {
        if(task_index != cur_task_index)
        {
            cur_task_index = task_index;
            cur_dialog_index = 1;
        }

        dialogs.Add("task" + cur_task_index + "_" + cur_dialog_index, text);
        cur_dialog_index++;
    }

    public static void Init(SystemLanguage lang)
    {
        pets = new Dictionary<string, string>();
        other_strings = new Dictionary<string, string>();

        if (lang != SystemLanguage.Russian)
        {
            //коты 
            pets.Add("avtoritet_cat", "\"This is Sparta!\"");
            pets.Add("bagira_cat", "Rope");
            pets.Add("bandit_cat", "WTF");
            pets.Add("beliy_cat", "White");
            pets.Add("besheniy_cat", "Badass");
            pets.Add("Borziy_cat", "\"Who's there?\"");
            pets.Add("boss_cat", "Boss");
            pets.Add("botanic_cat", "Nerd");
            pets.Add("chetkiy_cat", "Greedy");
            pets.Add("derzkiy_cat", "LOL");
            pets.Add("gamer_cat", "Gamer");
            pets.Add("hitriy_cat", "Asian");
            pets.Add("interesuyushiysya_cat", "Shy");
            pets.Add("jajdushiy_cat", "Hangover");
            pets.Add("kak_tak_cat", "\"Are you kidding me ? \"");
            pets.Add("karate_cat", "Karate");
            pets.Add("korolevskiy_cat", "Slowpoke");
            pets.Add("leps_cat", "PIMP");
            pets.Add("mafia_cat", "\"To be or not to be\"");
            pets.Add("mileyshiy_cat", "Curious");
            pets.Add("miliy_cat", "Cute");
            pets.Add("ofigevshiy_cat", "OMG");
            pets.Add("otvalite_cat", "\"Get off!\"");
            pets.Add("schastliviy_cat", "Happy");
            pets.Add("sonniy_cat", "Sleepy");
            pets.Add("ustaliy_cat", "Exhausted");
            pets.Add("vlubivshiysya_cat", "Loving");
            pets.Add("zadumavshiysa_cat", "Dreaming");
            pets.Add("zdaus-zdaus_cat", "Surrendering");
            pets.Add("zevaushiy_cat", "Yawning");
            pets.Add("aaa_cat", "\"AAAAAAAA!\"");
            pets.Add("abstracted_cat", "Tired");
            pets.Add("busy_cat", "Busy");
            pets.Add("despair_cat", "Despairing");
            pets.Add("filth_cat", "Saboteur");
            pets.Add("foreveralone_cat", "Forever alone");
            pets.Add("freshmind_cat", "Fresh eyes");
            pets.Add("friendly_cat", "Friendly");
            pets.Add("givemefive_cat", "High five");
            pets.Add("hacker_cat", "Hacker");
            pets.Add("helpless_cat", "Helpless");
            pets.Add("hey_yo_cat", "\"Hey, yo!\"");
            pets.Add("iwantparty_cat", "Party");
            pets.Add("kung_fu_cat", "Kung Fu");
            pets.Add("lalala_cat", "Lalala");
            pets.Add("loyal_cat", "Calm");
            pets.Add("monorail_cat", "Monorail");
            pets.Add("mousepad_cat", "Mousepad");
            pets.Add("piano_cat", "Piano");
            pets.Add("pocket_cat", "Pocket");
            pets.Add("scotchtape_cat", "Duct tape");
            pets.Add("superhero_cat", "Superhero");
            pets.Add("teamplay_cat", "Team play");
            pets.Add("vegan_cat", "Vegan");
            pets.Add("what_did_u_say_cat", "\"What did you say ?\"");
        }
        else
        {
            //коты
            pets.Add("avtoritet_cat", "\"Это Спарта!\"");
            pets.Add("bagira_cat", "Шланг");
            pets.Add("bandit_cat", "WTF");
            pets.Add("beliy_cat", "Белый");
            pets.Add("besheniy_cat", "Дерзкий");
            pets.Add("Borziy_cat", "\"Кто здесь ? \"");
            pets.Add("boss_cat", "Босс");
            pets.Add("botanic_cat", "Ботаник");
            pets.Add("chetkiy_cat", "Жадный");
            pets.Add("derzkiy_cat", "Ахаха");
            pets.Add("gamer_cat", "Геймер");
            pets.Add("hitriy_cat", "Китаися");
            pets.Add("interesuyushiysya_cat", "Скромняжка");
            pets.Add("jajdushiy_cat", "Сушняк");
            pets.Add("kak_tak_cat", "Фигасе");
            pets.Add("karate_cat", "Каратэ");
            pets.Add("korolevskiy_cat", "Слоупок");
            pets.Add("leps_cat", "Сутенер");
            pets.Add("mafia_cat", "Быть или не быть");
            pets.Add("mileyshiy_cat", "Любознательный");
            pets.Add("miliy_cat", "Мимими");
            pets.Add("ofigevshiy_cat", "\"Что там ? \"");
            pets.Add("otvalite_cat", "Отвалите");
            pets.Add("schastliviy_cat", "Счастливый");
            pets.Add("sonniy_cat", "Сонный");
            pets.Add("ustaliy_cat", "Усталость 80 lvl");
            pets.Add("vlubivshiysya_cat", "Нежный");
            pets.Add("zadumavshiysa_cat", "Мечтатель");
            pets.Add("zdaus-zdaus_cat", "Сдаюсь-сдаюсь");
            pets.Add("zevaushiy_cat", "Зевающий");
            pets.Add("aaa_cat", "АААААААА");
            pets.Add("abstracted_cat", "Рассеянный");
            pets.Add("busy_cat", "Занятой");
            pets.Add("despair_cat", "Обреченный");
            pets.Add("filth_cat", "Пакостник");
            pets.Add("foreveralone_cat", "Фореверэлоун");
            pets.Add("freshmind_cat", "Свежий взгляд");
            pets.Add("friendly_cat", "Дружелюбный");
            pets.Add("givemefive_cat", "\"Дай пять!\"");
            pets.Add("hacker_cat", "Хакер");
            pets.Add("helpless_cat", "Беспомощный");
            pets.Add("hey_yo_cat", "\"Хэй, йо!\"");
            pets.Add("iwantparty_cat", "Тусовщик");
            pets.Add("kung_fu_cat", "Кунгфу");
            pets.Add("lalala_cat", "Лалала");
            pets.Add("loyal_cat", "Спокойный");
            pets.Add("monorail_cat", "Монорельс");
            pets.Add("mousepad_cat", "Коврик");
            pets.Add("piano_cat", "Маэстро");
            pets.Add("pocket_cat", "Карманный");
            pets.Add("scotchtape_cat", "Скотч");
            pets.Add("superhero_cat", "Герой");
            pets.Add("teamplay_cat", "Командный");
            pets.Add("vegan_cat", "Веган");
            pets.Add("what_did_u_say_cat", "\"Чо ты сказал ? \"");
        }

        dialogs = new Dictionary<string, string>();
        task_names = new Dictionary<string, string>();
        texts = new Dictionary<string, string>();

        if (lang != SystemLanguage.Russian)
        {
            AddDialog(1, "Hello! I'm a cat, let's be friends!");
            AddDialog(1, "Dear friend, this is my grandmother's house, we can live there. We have to put our paws on it to make it nice.");
            AddDialog(1, "First, let's unblock the entrance and then look inside.");

            AddDialog(2, "Hi, I'm Blacky. I saw that you were doing something in this abandoned house, I got interested and decided to ask.");
            AddDialog(2, "Hi! I'm a cat, and this is our friend. My grandma used to live in this house and I want like to restore it");
            AddDialog(2, "But I do not know where to start? ");
            AddDialog(2, "Of course, we’ll start with the kitchen! Anyway, we need to eat some snacks while repairing the house! ");
            
            AddDialog(3, "What a cool kitchen!");
            AddDialog(3, "Yeah. But it’s not comfy to be standing on the bare concrete floor...");

            AddDialog(4, "What’s left is to get the walls done and we’re ready for guests.");
            AddDialog(4, "By the way, almost the whole interior can be changed if bored. To change, press 2 times on the object you want to change.");
            AddDialog(4, "Well, we finished with the kitchen. Let's get the garbage off the street? ");                                                          

            AddDialog(7, "I ordered the removal of garbage, I'll have to wait. But if you spend a few coins, you do not have to wait");
            AddDialog(7, "Now it’s clean on the lawn in front of the house! Let's finish the housework ");
            AddDialog(7, "We will need coins to go on with repairing. You can earn them here");

            AddDialog(30, "I'll call my friends! Together we can do more and we'll always be able to get some rest in the living room!");
            AddDialog(30, "Cool, I like the idea of having a rest now!");
            AddDialog(30, "It's nice to receive gifts! It’s important that there’s going to be more gifts, you just have to be into the game every day :)");
            AddDialog(30, "Let’s continue working on the house!");
            AddDialog(30, "Let's sort out all the stuff and put up wallpaper.");
            AddDialog(30, "The first booster for games is open. Let’s try it!");                                             

            AddDialog(9, "Here's your order.");
            AddDialog(9, "We are also glad to inform that your order has won an invitation to a cat exhibition in or lottery.");
            AddDialog(9, "Thank you!");
            AddDialog(9, "I'll call my friends!");
            AddDialog(9, "Together we can do more and we'll always be able to get some rest in the living room!");
            AddDialog(9, "Furniture is booked! I can't wait.");
            AddDialog(9, "Maybe we can try and get some coins in the games to accelerate it?");

            AddDialog(10, "Well, how were the exhibitions?");
            AddDialog(10, "Everything was marvelous!");
            AddDialog(10, "Now, we just need to get some more coins to dress up and get more beauty points!");
            AddDialog(10, "How are you doing?");
            AddDialog(10, "We decided we had to get remove the blockage before the bathroom and in the bedroom and move ahead doing the house!");
            AddDialog(10, "Excellent! Then let's earn some stars and go ahead!");
            AddDialog(10, "Hello! You won a trip to cat exhibitions!");
            AddDialog(10, "To get there you need to click on the exhibition icon.");
            AddDialog(10, "You have a guide in your invitation.");
            //10
            AddDialog(10, "It says you can attend an exhibition for 1 energy.");
            AddDialog(10, "While visiting an exhibition I get beauty points. They are summed.");
            AddDialog(10, "And after the exhibition I will get prizes and go up in the rating!");
            AddDialog(10, "First I will just try to attend.");
            AddDialog(10, "It also says I can get more beauty points with a cat dressed in a wardrobe.");
            AddDialog(10, "Excellent! Now I'm looking cool and will get more beauty points.");
            AddDialog(10, "Luckily there is a new exhibition just started! We have to go and show quickly!");
            AddDialog(10, "By the way, I can go back home the same way I got the the exhibition, with a homework button.");

            AddDialog(12, "Look, there are some locks on the door. We must open them. But how to?");
            AddDialog(12, "Let's call my friend Jackie, he's got long, thin claws and he can open any lock!");
            AddDialog(12, "Great idea!");

            AddDialog(13, "Нам открылся первый бустер для игр. Сейчас попробуем его в деле.");

            AddDialog(14, "Hello! I'm Jackie. Blackie said you needed help with the locks.");
            AddDialog(14, "Hello! Yes, we have some doors locked and we need to open them.");
            AddDialog(14, "Then I'll do it!");
            AddDialog(14, "Ugh! It's a cool toilet!");
            AddDialog(14, "I've got a garden with a playground and some barn!");
            AddDialog(14, "Look, there's another garden behind the children's room!");
            AddDialog(14, "That's right! Let's remove the blockage in the playground and try to get there.");

            AddDialog(15, "We need to build a children's room! Then we will have more cats!");
            AddDialog(15, "Then I'll do it!");
            AddDialog(15, "That's cooooooool! There's a lot of jobs to do!");
            AddDialog(15, "Oh, my! A pond! We'll have our pond! And even our own garden!");
            AddDialog(15, "I think there are some instruments, so let's try to open it.");

            AddDialog(16, "It's so nice to see a lot of cats in the Grandma's house again!");

            AddDialog(17, "Oh! This lock is so difficult my claws are useless. Are you sure you don't have a key?");
            AddDialog(17, "Wait, Grandma told me once when they were kids they used to hide their toys in a tree hole!");
            AddDialog(17, "So let's see what's in there.");
            AddDialog(17, "Got it! Here it is.");

            AddDialog(18, "Come on! I ordered a library and a player.");
            AddDialog(18, "Cool! It's always fun to do household chores with music. And the library is useful for education!");

            AddDialog(19, "Wow, a lot of things! We can fix the swing, and there are disassembled benches and some instruments to repair the pavement!");

            AddDialog(20, "The flowers are restored! Perhaps we need to put in some seeds and grow flowers.");
            AddDialog(20, "Yes, that's what we're going to do!");

            AddDialog(22, "It is so good to walk and play in your own backyard! What could be better?");

            AddDialog(24, "It is a real paradise!");
            AddDialog(24, "It is so nice to seat by the pond and relax.");
            AddDialog(24, "Yes. We'll have a really nice pond.");

            AddDialog(25, "We restored Grandma's house!");
            AddDialog(25, "Yes, we did it! So cool!");
            AddDialog(25, "Well, still a lot to improve but even now the house is just awesome.");
            AddDialog(25, "Look, so many kittens are there! We got a super cool house for kittens!");
            AddDialog(25, "Maybe we should do it.");
            AddDialog(25, "To adopt kittens?");
            AddDialog(25, "Yes! Just look, it is great here! Everybody likes it!");
            AddDialog(25, "Yes! Well, what we will do next?");
            AddDialog(25, "We'll wait for the update with a city map!");
            AddDialog(25, "That's right! Meantime, we will try to buy the best things for our house abd and to win all cat exhibitions!");


            task_names.Add(1.ToString(), "We need to get  into the house. Clean up all the stuff that stop you at entrance");
            task_names.Add(2.ToString(), "Restore the kitchen to secure a place to eat");
            task_names.Add(4.ToString(), "Restore the floor in the kitchen");
            task_names.Add(5.ToString(), "Restore the walls in the kitchen");
            task_names.Add(7.ToString(), "Garbage removal");
            task_names.Add(8.ToString(), "Lay a floor in the house");
            task_names.Add(9.ToString(), "Order furniture");
            task_names.Add(10.ToString(), "Learn Cat Exhibitions");
            task_names.Add(11.ToString(), "Replace wallpapers");
            task_names.Add(12.ToString(), "Blockage in the bedroom and toilet");
            task_names.Add(13.ToString(), "Restore the Bedroom");
            task_names.Add(14.ToString(), "Open the toilet and the second garden");
            task_names.Add(15.ToString(), "Examine boxes in the children's room");
            task_names.Add(16.ToString(), "Build the children's room");
            task_names.Add(17.ToString(), "Open the barn");
            task_names.Add(18.ToString(), "Install the Library and Music");
            task_names.Add(19.ToString(), "Open the barn");
            task_names.Add(20.ToString(), "Flowers in the first garden");
            task_names.Add(21.ToString(), "Paving stones in the first garden");
            task_names.Add(22.ToString(), "Repair the swing and restore the play room in the second garden");
            task_names.Add(23.ToString(), "Install benches in the first garden");
            task_names.Add(24.ToString(), "Restore the pond in the first garden");
            task_names.Add(25.ToString(), "Finish chapter 1");

            //initializer
            texts.Add("initializer_btn_play", "PLAY!");
            texts.Add("initializer_loading", "Loading");

            //bubble
            texts.Add("bubble_task_3_1", "Feels so good and comfy to my paws!");
            texts.Add("bubble_thansk", "Thanks, dear friend");

            //scanning
            texts.Add("header_make", "Make photo");
            texts.Add("Photo_ResultScreen_rang_header_text", "Cat-rank");
            texts.Add("Main_ScanScreen_rang_title_text", "Cat-rank");            
            texts.Add("header_scan", "Go scanning?");
            texts.Add("scanning_header_text", "Scanning");
            texts.Add("scanning_btn_text", "push");
            texts.Add("star_rule_text", "Scan and get a new star every five new cats!");
            texts.Add("Photo_ResultScreen_header_text", "Now you are");
            texts.Add("cate", "cat");
            texts.Add("next_star_1", "The next star will be obtained after ");
            texts.Add("next_star_2", " new cats");
            texts.Add("get_star_text", "You got a star!");
            texts.Add("photo_goals_title_text", "GOALS");
            texts.Add("photo_goals_btn_text", "Start");
            texts.Add("photo_goals_body_text", "You need open %N% new cats!");
            texts.Add("pickup_btn_text", "Pickup");
            texts.Add("pickup_panel_btn_text", "Tap to continue");
            texts.Add("Photo_ResultScreen_pu_header_1_text", "New");
            texts.Add("Photo_ResultScreen_pu_header_2_text", "Cat-rank");
            texts.Add("Photo_ResultScreen_pu_btn_text", "Continue");

            //mm_scanning
            texts.Add("open_cats", "Cats opened ");
            texts.Add("Main_ScanScreen_btn_text_text", "Scanning!");
            texts.Add("Main_ScanScreen_title_text", "Scanning");
            texts.Add("Main_ScanScreen_rang_aim_text", "Open %N% of new cats.");
            texts.Add("Main_ScanScreen_rang_max_text", "You have reached the highest rank!");
            texts.Add("Photo_ResultScreen_pb_next_rang_text_text", "Open %N% of new cats.");

            //ranks
            texts.Add("cat_rang_1_text", "Beginner CatScientist");
            texts.Add("cat_rang_2_text", "Advanced CatScientist");
            texts.Add("cat_rang_3_text", "Bachelor of CatSciences");
            texts.Add("cat_rang_4_text", "Expert in CatSciences");
            texts.Add("cat_rang_5_text", "Master of CatSciences");
            texts.Add("cat_rang_6_text", "PhD  in CatSciences");
           
            //mm_tasks
            texts.Add("mm_tasks_header_text", "List of missions");
            texts.Add("mm_tasks_chapter_text", "Chapter %N%");
            

            //mm_minigames
            texts.Add("mm_minigames_header_text", "GAMES");
            texts.Add("mm_minigames_coins_game_text", "Earn coins");
            texts.Add("mm_minigames_stars_game_text", "Win stars");
            texts.Add("mm_minigames_fail_rule_text", "If you don't reach your objective");
            texts.Add("mm_minigames_aims_text", "Aim: ");
            texts.Add("mm_minigames_level_text", "Level ");
            texts.Add("mm_minigames_best_text", "Best results");
            texts.Add("mm_minigames_play_btn_text", "Play");
            texts.Add("mm_minigames_COINS_text", "COINS");
            texts.Add("mm_minigames_POINTS_text", "POINTS");
            texts.Add("mm_minigames_TIME_text", "TIME");
            texts.Add("mm_minigames_in_seconds_text", "IN %N% SECONDS");            
            texts.Add("mm_boosters_upgr_header_text", "Upgrading Boosters");
            texts.Add("mm_boosters_upgr_btn_text", "Upgrade");
            texts.Add("mm_boosters_upgr_buy_panel_header_text", "Buy");


            //minigames
            texts.Add("minigame_fail_header_text", "PITTY...");
            texts.Add("minigame_fail_body_text", "You don't have enough hearts");
            texts.Add("minigame_fail_footer_text", "Buy another chance or get it after watching the video");
            texts.Add("minigame_brokenheard_header_text", "Oops!");
            texts.Add("minigame_brokenheard_body_text", "You spent one heart");
            texts.Add("minigame_brokenheard_btn_text", "Retry");
            texts.Add("minigame_winstar_cont_btn_text", "Tap to continue");
            texts.Add("minigame_winstar_header_text", "Level");
            texts.Add("minigame_winstar_body_text", "PASSED!");
            texts.Add("minigame_winstar_btn_text", "Tap to continue");
            texts.Add("minigame_goals_up_text", "Whatever you wish!");
            texts.Add("minigame_goals_down_text", "Whatever you wish!");
            texts.Add("minigame_goals_header_text", "OBJECTIVES");
            texts.Add("minigame_goals_footer_text", "Have fun!");
            texts.Add("minigame_goals_btn_text", "Start!");
            texts.Add("minigame_goals_seconds_text", "seconds");
            texts.Add("minigame_result_header_text", "Well done!");
            texts.Add("minigame_result_btn_text", "Continue?");
            texts.Add("minigame_increase_header_text", "Make a better result?");
            texts.Add("minigame_tutor_tap_text", "Click on the screen");
            texts.Add("minigame_tutor_action_tap_text", "The ribbon will turn");
            texts.Add("minigame_tutor_action_zig_text", "The cat will turn");
            texts.Add("minigame_goals_stars_header_text", "GOALS");
            texts.Add("minigame_goals_stars_goal_text", "Achive goals with a smile!");
            texts.Add("minigame_goals_stars_btn_text", "Start!");
            texts.Add("minigames_quest_booster_header_text", "Any questions?");
            texts.Add("minigames_quest_booster_body_text", "Play on and I'll tell you how to use it a little bit later!");
            texts.Add("minigames_buy_booster_panel_header_text", "Buy");
            texts.Add("minigames_boosters_title", "Boosters are in the game");

            //catshow
            texts.Add("catshow_result_yes_text", "Yeah!");
            texts.Add("catshow_result_btn_cont_text", "Continue!");
            texts.Add("catshow_result_btn_text", "Next exhibition!");
            texts.Add("catshow_result_header_text", "Well done!");
            texts.Add("catshow_goal_header_text", "OBJECTIVES");
            texts.Add("catshow_goal_btn_text", "Play");
            texts.Add("catshow_goal_body_text", "It all depends on you!");
            texts.Add("catshow_goal_footer_text", "Your clothes make ");
            texts.Add("catshow_fail_header_text", "Oops...");
            texts.Add("catshow_fail_body_text", "You have no energy");
            texts.Add("catshow_fail_footer_text", "Buy energy for coins or get it for watching video");
            texts.Add("catshow_tutor_left_text", "Swipe Left");
            texts.Add("catshow_tutor_right_text", "Swipe Right");
            texts.Add("catshow_tutor_up_text", "Swipe Up");
            texts.Add("catshow_tutor_down_text", "Swipe Down");
            texts.Add("catshow_tutor_header_text", "Tutorial");

            //mm
            texts.Add("mm_hearts_header_text", "OOPS!");
            texts.Add("mm_hearts_body_text", "Wait till they restore or buy some or watch the video.");
            texts.Add("mm_hearts_add_btn_text", "Need more?");
            texts.Add("mm_coins_header_text", "NEED MORE COINS?");
            texts.Add("mm_coins_body_text", "Buy using the bank or get it after watching a video!");
            texts.Add("mm_coins_play_btn_text", "Free");
            texts.Add("mm_coins_bank_btn_text", "Bank");            
            texts.Add("mm_coins_add_btn_text", "Need more?");
            texts.Add("mm_energy_header_text", "Opps!");
            texts.Add("mm_energy_body_text", "Wait till they restore or buy some or watch the video.");
            texts.Add("mm_stars_header_text", "YOU DO NOT HAVE ENOUGH STARS...");
            texts.Add("mm_stars_body_text", "Earn stars \nwhile playing mini games or scanning!");
            texts.Add("mm_stars_play_btn_text", "Play");
            texts.Add("mm_regard_coins_header_text", "Reward");
            texts.Add("mm_regard_coins_btn_text", "Take it!");
            texts.Add("mm_regard_hearts_header_text", "Reward");
            texts.Add("mm_regard_hearts_btn_text", "Take it!");
            texts.Add("mm_customizer_header_text", "Customize");
            texts.Add("mm_customize_tutor_text", "Double tap on the item you want to change.");
            texts.Add("mm_namepanel_title_text", "What about getting to know each other? ");
            texts.Add("mm_namepanel_placeholder_text", "What is your name?");
            texts.Add("mm_rate_rate_title_text", "Well, how do you like it?");
            texts.Add("mm_rate_rate_body_text", "Please rate the game about the Cat");
            texts.Add("mm_rate_rate_fail_body_text", "Thank you! Your feedback is very important to us!");
            texts.Add("mm_rate_market_title_text", "Probably…»");
            texts.Add("mm_rate_market_body_text", "Please leave a review on the Play Market? ");
            texts.Add("mm_rate_market_btn_text", "Leave a review!");
            texts.Add("mm_sn_title_text", "Funny funs!");
            texts.Add("mm_sn_body_text", "Want to get the latest news first? Post pictures of their cats and participate in competitions you want? Then subscribe!");
            texts.Add("mm_sn_no_btn_text", "No");
            texts.Add("mm_sn_later_btn_text", "Later");
            texts.Add("mm_sn_subs_btn_text", "Sign!");
            texts.Add("Main_StarsScreen_header_text", "NOT ENOUGH STARS...! ");
            texts.Add("Main_StarsScreen_title1_text", "Need more stars?");
            texts.Add("Main_StarsScreen_title_game_text", "Play \nGames");
            texts.Add("Main_StarsScreen_title_stars_text", "Earn \nStars");
            texts.Add("Main_StarsScreen_title_tasks_text", "Complete \ntasks!");
            texts.Add("Main_StarsScreen_btn_text_text", "Play!");
            texts.Add("Main_ScanTicketsScreen_header_text", "NEED SCAN TICKETS");
            texts.Add("Main_ScanTicketsScreen_title1_text", "Wanna scan?");
            texts.Add("Main_ScanTicketsScreen_title_tasks_text", "Complete \ntasks");
            texts.Add("Main_ScanTicketsScreen_title_scan_text", "Get scan \ntickets");
            texts.Add("Main_ScanTicketsScreen_title_rang_text", "Scan \nwhatever");
            texts.Add("Main_ScanTicketsScreen_btn_text_text", "Ok");

            //cs_catshow
            texts.Add("cs_catshow_header_text", "Cat Exhibitions");
            texts.Add("cs_catshow_game_info_text", "Attend exhibitions, get beauty points and win prizes.");
            texts.Add("cs_catshow_game_btn_text", "Play");
            texts.Add("cs_catshow_rating_btn_text", "Rating");
            texts.Add("cs_catshow_rating_prize_btn_text", "Prizes by rating");
            texts.Add("cs_catshow_ponts_prize_btn_text", "Prizes by \npoints");
            texts.Add("cs_catshow_pnts_points_text", "Score");
            texts.Add("cs_catshow_pnts_prize_text", "Prize");
            texts.Add("cs_catshow_rt_place_text", "Place");
            texts.Add("cs_catshow_rt_name_text", "Name");
            texts.Add("cs_catshow_rt_points_text", "Score");
            texts.Add("cs_catshow_rtp_place_text", "Your \nplace in");
            texts.Add("cs_catshow_rtp_prize_text", "Prize");
            texts.Add("cs_catshow_end_show_text", "Results of the Exhibitions after ");
            texts.Add("cs_catshow_end_show_day_text", " days ");
            texts.Add("cs_catshow_end_show_hours_text", " hours ");
            texts.Add("cs_catshow_end_show_minutes_text", " minutes ");
            texts.Add("cs_catshow_end_show_seconds_text", " seconds");

            //cs
            texts.Add("cs_coins_header_text", "NEED MORE COINS?");
            texts.Add("cs_coins_body_text", "Play games or you get a reward for watching the video!");
            texts.Add("cs_coins_add_btn_text", "Need more?");
            texts.Add("cs_energy_header_text", "Oops!");
            texts.Add("cs_energy_body_text", "Wait till they restore or buy some or watch the video.");
            texts.Add("cs_energy_add_btn_text", "Need more?");
            texts.Add("cs_energy_regard_header_text", "Reward");
            texts.Add("cs_energy_regard_btn_text", "Take it!");

            //shop
            texts.Add("sc_shop_header_text", "Store");
            texts.Add("sc_shop_head_btn_text", "Hats");
            texts.Add("sc_shop_collar_btn_text", "Collars");
            texts.Add("sc_shop_skeen_btn_text", "Shades");
            texts.Add("sc_shop_eye_btn_text", "Eye Lenses");
            texts.Add("sc_shop_glasses_btn_text", "Glasses");

            //dialog
            texts.Add("dialog_next_btn_text", "Next");
            texts.Add("dialog_mission_area_title_text", "New mission");
            

            //prize
            texts.Add("prize_points_header_text", "Oh, wow!");
            texts.Add("prize_points_points_text", "You got");
            texts.Add("prize_points_prize_text", "Your prize");
            texts.Add("prize_points_btn_text", "Take it!");
            texts.Add("prize_week_header_text", "Results of the Exhibitions");
            texts.Add("prize_week_points_text", "Collected");
            texts.Add("prize_week_place_text", "Place");
            texts.Add("prize_week_percent_place_text", "Place in");
            texts.Add("prize_week_prize_text", "Your prize");
            texts.Add("prize_week_btn_text", "Take it!");
            texts.Add("mm_daily_prize_header", "Gifts!");
            texts.Add("mm_daily_prize_day", "Day!");
            texts.Add("mm_daily_prize_button_open_text", "Open");
            texts.Add("mm_daily_prize_button_pick_up_text", "Pick up");
            texts.Add("mm_daily_prize_next_text", "The next gift will appear in %N% of hours");




            other_strings.Add("mm_sn_icon_name", "icon_SN_EN");
            other_strings.Add("mm_rate_url", "https://play.google.com/store/apps/details?id=com.scannerapps.whatCat&hl=en");
            other_strings.Add("social_url", "https://www.facebook.com/HappyGamesApp/");

            //boosters
            texts.Add("booster_reborn_description", "You can go on playing even if you fall!");
            texts.Add("booster_supercat_description", "Flies without obstacles% N% meters!");
            texts.Add("booster_magnet_description", "Attracts all the coins next to the seal.");
            texts.Add("booster_reborn_name", "Cat's life");
            texts.Add("booster_supercat_name", "Supercat");
            texts.Add("booster_magnet_name", "Banker’s magnet");
            texts.Add("booster_reborn_upgrate_description", "%N% times per game");
            texts.Add("booster_magnet_upgrate_description", "%N% seconds»");
            texts.Add("booster_supercat_upgrate_description", "%N% meters");

            //push
            texts.Add("push_locale_text", "en");
            texts.Add("push_1_text", "Cats prepared a gift! Come and get it!");
            texts.Add("push_2_text", "Please come help the cats. The grandmother's house still needs a lot of work to be done.");
            texts.Add("push_3_text", "It's time to go back and earn a couple of stars!");
            texts.Add("push_4_text", "It's been a week since we met. Hooray ;)");
            texts.Add("push_5_text", "Cats prepared a gift! Come and get it!");
            texts.Add("push_6_text", "Please come help the cats. The grandmother's house still needs a lot of work to be done.");
            texts.Add("push_7_text", "It's time to go back and earn a couple of stars!");
            texts.Add("push_8_text", "Gifts delivery is on the alert! Come in and take it!");
            texts.Add("push_9_text", "Cats prepared a gift! Come and get it!");
            texts.Add("push_10_text", "Please come help the cats. The grandmother's house still needs a lot of work to be done");
            texts.Add("push_11_text", "It's time to go back and earn a couple of stars!");
            texts.Add("push_12_text", "Delivery of gifts does not sleep! Come in, take it! ");
            texts.Add("push_13_text", "Cats prepared a gift! Come and get it! ");
            texts.Add("push_14_text", "Please come help the cats. The grandmother's house still needs a lot of work to be done");
            texts.Add("push_15_text", "It's time to go back and earn a couple of stars! ");
            texts.Add("push_16_text", "I'm so glad that a month ago it happened that we met!");

            //bubble
            texts.Add("bubble_WON_MINIGAME_0", "you’re so good at coping with the game!");
            texts.Add("bubble_WON_MINIGAME_1", "Always a winner!");
            texts.Add("bubble_WON_MINIGAME_2", "Your skills make me wow!");
            texts.Add("bubble_WON_MINIGAME_3", "Tell me how to be so cool?");
            texts.Add("bubble_WON_MINIGAME_4", "I knew you were going to win!");
            texts.Add("bubble_WON_MINIGAME_5", "Have you just seen it? It was so cool!");
            texts.Add("bubble_WON_MINIGAME_6", "Another star in your piggy bank!");
            texts.Add("bubble_WON_MINIGAME_7", "Not sure if there’s someone cooler than you...");
            texts.Add("bubble_WON_MINIGAME_8", "Always dreamed about such a friend as you! ");
            texts.Add("bubble_WON_MINIGAME_9", "How ?! How do you cope with this?");

            texts.Add("bubble_LOSE_MINIGAME_0", "We believe in you, don’t get down.");
            texts.Add("bubble_LOSE_MINIGAME_1", "You can cope with all the failures!");
            texts.Add("bubble_LOSE_MINIGAME_2", "Don’t give up and everything will work out!");
            texts.Add("bubble_LOSE_MINIGAME_3", "You're the coolest anyway! ");
            texts.Add("bubble_LOSE_MINIGAME_4", "This is not the last attempt, there you go! ");
            texts.Add("bubble_LOSE_MINIGAME_5", "As for me, 5 seconds to survive is already a challenge!");
            texts.Add("bubble_LOSE_MINIGAME_6", "Don’t be sad, I do not know anybody who had only one try and succeeded.");
            texts.Add("bubble_LOSE_MINIGAME_7", "Skip it, that’s all right. Let's prepare for the next attempt!");
            texts.Add("bubble_LOSE_MINIGAME_8", "Do not get down, it will work out next time!");
            texts.Add("bubble_LOSE_MINIGAME_9", "Get over it! Victory is close at hand! ");

            texts.Add("bubble_SCANNED_0", "Wow! Such funny scans! ");
            texts.Add("bubble_SCANNED_1", "Oh, sometimes it feels like getting a better camera...");
            texts.Add("bubble_SCANNED_2", "It's a pity we do not have a test: \' what kind of person you are?/'  :)");
            texts.Add("bubble_SCANNED_3", "What kind of cat are you? You're a cool cat, that's all! ");
            texts.Add("bubble_SCANNED_4", "Sometimes I look at you and I see a cat");
            texts.Add("bubble_SCANNED_5", "The more you scan, the more you’re familiar with the cats.");
            texts.Add("bubble_SCANNED_6", "You are very funny!  :)");
            texts.Add("bubble_SCANNED_7", "Cheerful kittens are just like you!");

            texts.Add("bubble_NEW_RANK_REACHED_0", "Congratulations on the new rank!");

            texts.Add("bubble_tap_Main_0", "Cats like sleeping. 70% of the day is sleeping time");
            texts.Add("bubble_tap_Main_1", "Cats can create about 100 sounds.");
            texts.Add("bubble_tap_Main_2", "There are about 40 different breeds of domestic cats in the world.");
            texts.Add("bubble_tap_Main_3", "The first domestic cat has been found in Cyprus");
            texts.Add("bubble_tap_Main_4", "The first cat is an astronaut. It’s Felicette, she’s French");
            texts.Add("bubble_tap_Main_5", "Domestic cats can accelerate to 50 km/h");
            texts.Add("bubble_tap_Main_6", "Cats can jump 5 times higher than their height");
            texts.Add("bubble_tap_Main_7", "Scientists have no idea how we make purr sounds");
            texts.Add("bubble_tap_Main_8", "Cats have about 24 whiskers.");
            texts.Add("bubble_tap_Main_9", "Cats have a brilliant sense of vision in the dark.");
            texts.Add("bubble_tap_Main_10", "Cats can turn their ears by 180 degrees.");
            texts.Add("bubble_tap_Main_11", "The adult cat has 30 teeth. The kittens have 26.");
            texts.Add("bubble_tap_Main_12", "Cats can drink seawater");
            texts.Add("bubble_tap_Main_13", "It's so nice that we are all in one house now.");
            texts.Add("bubble_tap_Main_14", "How much do you know about cats? ");
            texts.Add("bubble_tap_Main_15", "So glad to see friends around");
            texts.Add("bubble_tap_Main_16", "purr – purr");
            texts.Add("bubble_tap_Main_17", "meow – meow");
            texts.Add("bubble_tap_Main_18", "Are we going to build something else?");
            texts.Add("bubble_tap_Main_19", "I like walking by the pond");
            texts.Add("bubble_tap_Main_20", "I'm afraid going back to town alone");
            texts.Add("bubble_tap_Main_21", "«I'm afraid going back to town alone.");
            texts.Add("bubble_tap_Main_22", "I don’t like swimming.");
            texts.Add("bubble_tap_Main_23", "I’ve once slept on a man’s head! ");
            texts.Add("bubble_tap_Main_24", "I like scratching the wallpaper");
            texts.Add("bubble_tap_Main_25", "Sometimes you want to travel");
            texts.Add("bubble_tap_Main_26", "I like to lie around under the sun");
            texts.Add("bubble_tap_Main_27", "Feels so nice when they pet me. Murrr.");

            texts.Add("bubble_tap_Jakky_0", "I'm Jackie The Sharp Claw");
            texts.Add("bubble_tap_Jakky_1", "Cats like sleeping. 70% of the day goes to sleep");
            texts.Add("bubble_tap_Jakky_2", "Nobody is better in doors than me");
            texts.Add("bubble_tap_Jakky_3", "I can get to the place where no one has been");
            texts.Add("bubble_tap_Jakky_4", "Some people think that I have never been to their home");

            texts.Add("bubble_tap_Black_0", "So cool that you are with us");
            texts.Add("bubble_tap_Black_1", "I'm very glad that we are building a cat's house.");
            texts.Add("bubble_tap_Black_2", "Sometimes cats tell some facts about themselves");
            texts.Add("bubble_tap_Black_3", "Your own house! I'm shocked :)");
            texts.Add("bubble_tap_Black_4", "I can chase my tail. And you? ");
            texts.Add("bubble_tap_Black_5", "I'm proud of helping you with the construction.");
            texts.Add("bubble_tap_Black_6", "I have a lot of contacts around here");
            texts.Add("bubble_tap_Black_7", "When I was little, I was darker.");

            texts.Add("bubble_customize_0", "It became cozy immediately! I would like to lie down here");
            texts.Add("bubble_customize_1", "That’s exactly what I expected");
            texts.Add("bubble_customize_2", "It's so nice that you care about us.");
            texts.Add("bubble_customize_3", "Wow! Thank you! ");
            texts.Add("bubble_customize_4", "It would be so boring here without you! ");
            texts.Add("bubble_customize_5", "You are the best designer! ");
            texts.Add("bubble_customize_6", "You know how to surprise! ");
            texts.Add("bubble_customize_7", "Oh! I love it! ");
            texts.Add("bubble_customize_8", "I love funny changes! ");
            texts.Add("bubble_customize_9", "Very cool, thanks! ");

            texts.Add("bubble_relax", "You can always relax here");
            texts.Add("bubble_books", "Thirst for knowledge is all about me :)");
            texts.Add("bubble_joy", "Looks good to the eye! Have to hide my little claws");
            texts.Add("bubble_game", "Hooray! You can play as you like! ");
            texts.Add("bubble_soft_floor", "How nice my paws!");

            texts.Add("bubble_money_0", "You can watch some video, and you’ll get more coins");
            texts.Add("bubble_money_1", "I like running just for fun in a coin game.");
            texts.Add("bubble_money_2", "The more coins you have, the more beautiful our home gets.");
            texts.Add("bubble_money_3", "If you are in the game every day, you get gifts. I love receiving gifts");

            texts.Add("bubble_no_speedup_0", "So tired of waiting ? Can we speed it up ? ");				
            texts.Add("bubble_no_speedup_1", "Acceleration seems to be not expensive. You can afford it if you earn some money.");
            texts.Add("bubble_no_speedup_2", "Sometimes, I look at this timer and it makes me fall asleep");
            texts.Add("bubble_no_speedup_3", "Eh! So tired of waiting. When is the time finally up? ");
            texts.Add("bubble_no_speedup_4", "Ugh, how long should I wait till the time’s up?");

            texts.Add("bubble_no_customize_0", "Don’t you get bored with the walls? ");
            texts.Add("bubble_no_customize_1", "I would like to change the kitchen");
            texts.Add("bubble_no_customize_2", "I would try a new floor");
            texts.Add("bubble_no_customize_3", "It seems like we have tried all the sofas.");
            texts.Add("bubble_no_customize_4", "Let's work on the home decoration? ");

            //bank
            texts.Add("Main_Bank_restore_purchases_btn_text_text", "Restore purchases");
            texts.Add("Main_Bank_header_text", "The Bank");

            //tm
            texts.Add("pp_tutor", "Tap 3 times on the product to place an order.");
            texts.Add("customer_tutor", "Tap on the buyer to sell the product.");
            texts.Add("pu_tutor", "Tap on the product to start production.");
            texts.Add("success_customers_goal_tutor", "At this level, you must have at least 12 satisfied customers!");
            texts.Add("all_customers_success_goal_tutor", "At this level, no customer will leave unhappy!");
        }
        else
        {
            AddDialog(1, "Привет! Я котик, давай дружить!");
            AddDialog(1, "Дорогой друг, вот дом моей бабушки, мы можем его занять. Придется приложить к нему лапы");
            AddDialog(1, "Для начала давай разберем вход, а потом осмотримся внутри");

            AddDialog(2, "Привет, я Черныш. Увидел, что ты что-то делаешь в этом заброшенном доме, и решил поинтересоваться.");
            AddDialog(2, "Привет! Я котик, а это наш друг. В этом доме когда-то жила моя бабушка, я хочу его восстановить.");
            AddDialog(2, "Только я не знаю с чего начать?");
            AddDialog(2, "Конечно же с кухни! Ведь надо будет что-то кушать, пока мы будем ремонтировать дом!");

            AddDialog(3, "Какая крутая кухня!");
            AddDialog(3, "Даааа. Только так неуютно стоять на  голом бетонном полу...");

            AddDialog(4, "Осталось только закончить со стенами и можно звать гостей.");
            AddDialog(4, "Кстати, почти весь интерьер можно менять, если надоест. Что бы поменять нажмите 2 раза на объект, который хотите поменять.");
            AddDialog(4, "Ну вот мы и закончили с кухней. Давай уберем мусор с улицы?");


            //AddDialog(3, "Кстати, звёзды можно будет получить играя в игры. Ты знал?");
            //AddDialog(3, "Мне очень понравилось играть!");
            //AddDialog(3, "Да! Играть круто, но если у тебя не получится пройти миссию, то ты потратишь сердечко.");
            //AddDialog(3, "Хотя это не страшно, сердечки восстанавливаются!");
            //AddDialog(3, "Да и всегда можно просто играть и зарабатывать монетки без звездочек!");

            //AddDialog(6, "Смотри, если два раза быстро нажать на предмет, то его можно изменить!");
            //AddDialog(6, "Пол, Стены, Кухня, все что мы с тобой строим можно менять!");
            //AddDialog(6, "Так вот зачем нам понадобятся монетки! Отлично! Ну что же, продолжим заниматься домом!");

            AddDialog(30, "Я позову друзей! Вместе мы сможем больше делать и еще всегда сможем отдохнуть в гостинной!");
            AddDialog(30, "Отлично я за то что бы отдохнуть!");
            AddDialog(30, "Приятно забирать подарки! А самое главное теперь подарков будет больше, просто надо заходить в игру каждый день :)");
            AddDialog(30, "Будем продолжать работать с домом!");
            AddDialog(30, "Давай разбирать завалы и клеить новые обои.");
            AddDialog(30, "Нам открылся первый бустер для игр. Сейчас попробуем его в деле.");

            AddDialog(7, "Я заказал вывоз мусора, придется подождать. Но если потратить немного монеток, то ждать не придется");
            AddDialog(7, "Теперь на газоне перед домом чисто! Давай закончим работу по дому!");
            AddDialog(7, "Нам понядобятся монетки, что бы делать ремонт дальше.Их можно будет заработать тут");

            AddDialog(9, "Вот ваш заказ.");
            AddDialog(9, "Так же рады сообщить, что ваш заказ выиграл в лотерею путёвку на кошачью выставку");
            AddDialog(9, "Спасибо!");
            AddDialog(9, "Я позову друзей!");
            AddDialog(9, "Вместе мы сможем больше делать и еще всегда сможем отдохнуть в гостинной!");
            AddDialog(9, "Мебель заказана! Так не хочется ждать...");
            AddDialog(9, "Может попробуем раздобыть монеток в играх что бы ускорить?");

            AddDialog(10, "Ну как прошли выставки?");
            AddDialog(10, "Все было просто супер! ");
            AddDialog(10, "Теперь надо только достать монеток, что бы круто наряжаться на выставки и получать больше очков красоты!");
            AddDialog(10, "А у вас как дела?");
            AddDialog(10, "Мы решили, что надо разобрать завалы перед туалетом и в спальне и дальше продвигаться по работе с домом!");
            AddDialog(10, "Отлично! Тогда давайте заработаем звёзд и продолжим!");
            AddDialog(10, "Здравствуйте! Вы выиграли путевку на кошачьи выставки!");
            AddDialog(10, "Для того что бы попасть на них вам потребуется нажать на иконку выставок.");
            AddDialog(10, "Вся инструкция в путёвке.");
            //10
            AddDialog(10, "В путёвке написано, что участие в выставке стоит 1 энерегию.");
            AddDialog(10, "За одну выставку я получаю очки красоты. Которые суммируются.");
            AddDialog(10, "И в конце выставки я могу получить призы, если достаточно продвинусь в рейтинге!");
            AddDialog(10, "Для начала я просто попробую поучаствовать.");
            AddDialog(10, "Так же тут написано, что для того что бы получать больше очков красоты, нужно наряжать котика в гардеробе.");
            AddDialog(10, "Отлично! Теперь я круто выгляжу и получу больше очков красоты.");
            AddDialog(10, "Тем более как раз сейчас запустилась новая выставка! Нужно скорее идтиы выступать!");
            AddDialog(10, "Кстати, вернуться домой я смогу так же как попал в выставки, через кнопку домашних заданий.");

            AddDialog(12, "Смотри ка, там какие то замки на дверях. Надо бы их открыть. Но как?");
            AddDialog(12, "Давай позовем моего друга Джеки, у него длинные тонкие коготки, которыми он умеет открывать любой замок!");
            AddDialog(12, "Отличная идея!");

            AddDialog(13, "Нам открылся первый бустер для игр. Сейчас попробуем его в деле.");

            AddDialog(14, "Привет! Я Джеки. Черныш говорил, что вам нужна помощь с замками.");
            AddDialog(14, "Привет! Да Именно так у нас тут запертые двери, нам нужно их открыть.");
            AddDialog(14, "Тогда я сделаю это!");
            AddDialog(14, "Уф! Да тут прикольный туалет!");
            AddDialog(14, "А у меня сад с игровой площадкой и каким-то сараем!");
            AddDialog(14, "Смотри, там за детской комнатой есть еще один сад!");
            AddDialog(14, "Точно! Давай скорее разбирать площадку и попробуем туда попасть.");

            AddDialog(15, "Детскую надо построить! Тогда у нас будут играть больше котиков!");
            AddDialog(15, "Тогда я сделаю это!");
            AddDialog(15, "Вот это дааааа! Тут работы выше крыши!");
            AddDialog(15, "Ничего себе! Пруд! У нас будет пруд! И даже свой сад!");
            AddDialog(15, "Наверняка в сарае есть инструменты, давай попробуем его открыть.");

            AddDialog(16, "Так приятно смотреть, что в бабушкином доме снова много котиков!");

            AddDialog(17, "Оу! Тут такой замок, что что мои коготки не помогут. Вы уверены, что ключа нет?");
            AddDialog(17, "Постойте, бабушка рассказывала, что в детстве они всегда прятали свои игрушки в дупле на дереве!");
            AddDialog(17, "Так давайте же посмотрим, что там?");
            AddDialog(17, "Я нашёл! Вот же он.");

            AddDialog(18, "Ну что! Я заказал сюда библиотеку и проигрыватель!");
            AddDialog(18, "Круто! С музыкой всегда веселее заниматься домашними делами. А библиотека полезна для образования!");

            AddDialog(19, "Ого сколько тут всего! Мы можем наладить качели, тут разобранные скамейки и инстурмент для ремонта мостовой!");

            AddDialog(20, "Клумбы отремонтированы! Наверное надо посадить сюда самых разных цветов?");
            AddDialog(20, "Да, мы так и сделаем!");

            AddDialog(22, "Гулять и играть на собственном заднем дворике! Что может быть лучше?");

            AddDialog(24, "Это просто райское местечко!");
            AddDialog(24, "На пруду круто будет сидеть и расслабляться.");
            AddDialog(24, "Да. Очень красивый пруд у нас будет.");

            AddDialog(25, "Мы восстановили бабушкин дом!");
            AddDialog(25, "Да!Очень круто получилось.");
            AddDialog(25, "Правда кое-что навреное можно улучшить, но даже сейчас дом просто потрясный.");
            AddDialog(25, "Смотри сколько здесь котят! У нас получился супер крутой дом для котят!");
            AddDialog(25, "Может быть стоит этим заняться?");
            AddDialog(25, "Бездомными котятами?");
            AddDialog(25, "Да! Только посмотри как здесь круто! Всем нравится!");
            AddDialog(25, "Да! Но что мы будем делать дальше?");
            AddDialog(25, "Дождемся обновления, где нам откроют карту города!");
            AddDialog(25, "Точно! А пока что попытаемся купить все самое лучшее для этого дома и выиграть все кошачьи выставки!");


            task_names.Add(1.ToString(), "Нужно попасть в дом. Разбери завал на входе");
            task_names.Add(2.ToString(), "Восстановите кухню, что бы вам было где кушать");
            task_names.Add(4.ToString(), "Восстановите пол на кухне");
            task_names.Add(5.ToString(), "Восстановите стены на кухне");
            task_names.Add(7.ToString(), "Вывоз мусора");
            task_names.Add(8.ToString(), "Постелить пол в доме");
            task_names.Add(9.ToString(), "Заказ мебели");
            task_names.Add(10.ToString(), "Знакомство с кошачьими выставками");
            task_names.Add(11.ToString(), "Замена обоев");
            task_names.Add(12.ToString(), "Завал спальная и туалет");
            task_names.Add(13.ToString(), "Восстановление спальни");
            task_names.Add(14.ToString(), "Открыть туалет и второй сад");
            task_names.Add(15.ToString(), "Разбор коробок в детской");
            task_names.Add(16.ToString(), "Стройка детской");
            task_names.Add(17.ToString(), "Открыть сарай");
            task_names.Add(18.ToString(), "Установка библиотеки и музыки");
            task_names.Add(19.ToString(), "Открыть Сарай");
            task_names.Add(20.ToString(), "Цветы в первом саду");
            task_names.Add(21.ToString(), "Брусчатка в первом саду");
            task_names.Add(22.ToString(), "Ремонт качелей и восстановление игровой во втором саду");
            task_names.Add(23.ToString(), "Установка скамеек в первом саду");
            task_names.Add(24.ToString(), "Восстановление пруда в первом саду");
            task_names.Add(25.ToString(), "Завершить первую главу!");

            //initializer
            texts.Add("initializer_btn_play", "ИГРАТЬ!");
            texts.Add("initializer_loading", "Загрузка");

            //bubble
            texts.Add("bubble_task_3_1", "Как приятно лапкам!");
            texts.Add("bubble_thansk", "Спасибо, дорогой друг");

            //scanning
            texts.Add("header_make", "Сделать фото");
            texts.Add("Photo_ResultScreen_rang_header_text", "Котэ-ранг");
            texts.Add("Main_ScanScreen_rang_title_text", "Котэ-ранг");
            texts.Add("header_scan", "Вперед сканить?");
            texts.Add("scanning_header_text", "Сканирование");
            texts.Add("scanning_btn_text", "жми");
            texts.Add("star_rule_text", "Сканируй и получай новую звезду каждые 5 новых котиков!");
            texts.Add("Photo_ResultScreen_header_text", "Сейчас ты"); 
            texts.Add("cate", "котэ");
            texts.Add("next_star_1", "Следующая звезда через ");
            texts.Add("next_star_2", " новых котиков");
            texts.Add("get_star_text", "Ты получил звезду!");
            texts.Add("photo_goals_title_text", "ЦЕЛИ");
            texts.Add("photo_goals_btn_text", "Начать");
            texts.Add("photo_goals_body_text", "Вам нужно открыть %N% новых котиков!");
            texts.Add("pickup_btn_text", "Забрать");
            texts.Add("pickup_panel_btn_text", "Нажмите чтобы продолжить");
            texts.Add("Photo_ResultScreen_pu_header_1_text", "Новый");
            texts.Add("Photo_ResultScreen_pu_header_2_text", "Котэ-ранг");
            texts.Add("Photo_ResultScreen_pu_btn_text", "Продолжить");

            //mm_scanning
            texts.Add("open_cats", "Открыто котиков ");
            texts.Add("Main_ScanScreen_btn_text_text", "Сканировать!");
            texts.Add("Main_ScanScreen_title_text", "Сканирование");           
            texts.Add("Main_ScanScreen_rang_aim_text", "Открыть %N% новых котиков.");
            texts.Add("Main_ScanScreen_rang_max_text", "Вы достигли высшего ранга!");
            texts.Add("Photo_ResultScreen_pb_next_rang_text_text", "Открыть %N% новых котиков.");

            //ranks
            texts.Add("cat_rang_1_text", "Начинающий Котовед");
            texts.Add("cat_rang_2_text", "Продвинутый Котовед");
            texts.Add("cat_rang_3_text", "Бакалавр Котовед");
            texts.Add("cat_rang_4_text", "Специалист Котовед");
            texts.Add("cat_rang_5_text", "Магистр Котовед");
            texts.Add("cat_rang_6_text", "Доктор Котоведческих наук");

            //mm_tasks
            texts.Add("mm_tasks_header_text", "Список Миссий");
            texts.Add("mm_tasks_chapter_text", "Глава %N%");

            //mm_minigames
            texts.Add("mm_minigames_header_text", "ИГРЫ");
            texts.Add("mm_minigames_coins_game_text", "Заработай монетки");
            texts.Add("mm_minigames_stars_game_text", "Выиграй звезды");
            texts.Add("mm_minigames_fail_rule_text", "Если не достигнешь цели");
            texts.Add("mm_minigames_aims_text", "Цели: ");
            texts.Add("mm_minigames_level_text", "Уровень ");
            texts.Add("mm_minigames_best_text", "Лучшие результаты");
            texts.Add("mm_minigames_play_btn_text", "Играть");
            texts.Add("mm_minigames_COINS_text", "МОНЕТЫ");
            texts.Add("mm_minigames_POINTS_text", "ОЧКИ");
            texts.Add("mm_minigames_TIME_text", "ВРЕМЯ");
            texts.Add("mm_minigames_in_seconds_text", "ЗА %N% СЕКУНД");
            texts.Add("mm_boosters_upgr_header_text", "Прокачка Бустеров");
            texts.Add("mm_boosters_upgr_btn_text", "Прокачать");
            texts.Add("mm_boosters_upgr_buy_panel_header_text", "Покупка");
            

            //minigames
            texts.Add("minigame_fail_header_text", "ОЧЕНЬ ЖАЛЬ...");
            texts.Add("minigame_fail_body_text", "У вас недостаточно сердечек");
            texts.Add("minigame_fail_footer_text", "Купити щанс или получите его после просмотра видео");
            texts.Add("minigame_brokenheard_header_text", "УПС!");
            texts.Add("minigame_brokenheard_body_text", "Вы потратили одно сердечко");
            texts.Add("minigame_brokenheard_btn_text", "Повторить");
            texts.Add("minigame_winstar_cont_btn_text", "Нажмите чтобы продолжить");
            texts.Add("minigame_winstar_header_text", "Уровень");
            texts.Add("minigame_winstar_body_text", "ПРОЙДЕН!");
            texts.Add("minigame_winstar_btn_text", "Нажмите чтобы продолжить");
            texts.Add("minigame_goals_up_text", "Сколько захочешь!");
            texts.Add("minigame_goals_down_text", "Сколько захочешь!");
            texts.Add("minigame_goals_header_text", "ЦЕЛИ");
            texts.Add("minigame_goals_footer_text", "Получай удовольствие!");
            texts.Add("minigame_goals_btn_text", "Начать!");
            texts.Add("minigame_goals_seconds_text", "секунд");
            texts.Add("minigame_result_header_text", "Отличная работа!");
            texts.Add("minigame_result_btn_text", "Продолжить?");
            texts.Add("minigame_increase_header_text", "Увеличить результат?");
            texts.Add("minigame_tutor_tap_text", "Нажми на экран");
            texts.Add("minigame_tutor_action_tap_text", "лента повернется");
            texts.Add("minigame_tutor_action_zig_text", "котик повернет");
            texts.Add("minigame_goals_stars_header_text", "ЦЕЛИ");
            texts.Add("minigame_goals_stars_goal_text", "Достигай цели с улыбкой!");
            texts.Add("minigame_goals_stars_btn_text", "Начать!");
            texts.Add("minigames_quest_booster_header_text", "Вопросики?");
            texts.Add("minigames_quest_booster_body_text", "Играй дальше и чуть позже я расскажу как этим пользоваться!");
            texts.Add("minigames_buy_booster_panel_header_text", "Покупка");
            texts.Add("minigames_boosters_title", "Бустеры в игре");

            //catshow
            texts.Add("catshow_result_yes_text", "Ура!");
            texts.Add("catshow_result_btn_cont_text", "Продолжить!");
            texts.Add("catshow_result_btn_text", "Следующая выставка!");
            texts.Add("catshow_result_header_text", "Отличная работа!");
            texts.Add("catshow_goal_header_text", "ЦЕЛИ");
            texts.Add("catshow_goal_btn_text", "Играть");
            texts.Add("catshow_goal_body_text", "Все зависит от тебя!");
            texts.Add("catshow_goal_footer_text", "Ваша одежда дает ");
            texts.Add("catshow_fail_header_text", "УПС...");
            texts.Add("catshow_fail_body_text", "У вас нет энергии");
            texts.Add("catshow_fail_footer_text", "Купи за монеты или получи за просмотр видео");
            texts.Add("catshow_tutor_left_text", "Свайпните влево");
            texts.Add("catshow_tutor_right_text", "Свайпните вправо");
            texts.Add("catshow_tutor_up_text", "Свайпните вверх");
            texts.Add("catshow_tutor_down_text", "Свайпните вниз");
            texts.Add("catshow_tutor_header_text", "Обучение");

            //mm
            texts.Add("mm_hearts_header_text", "УПС!");
            texts.Add("mm_hearts_body_text", "Подожди пока они восстановятся или купи или посмотри видео.");
            texts.Add("mm_hearts_add_btn_text", "Нужно еще?");
            texts.Add("mm_coins_header_text", "НУЖНО БОЛЬШЕ МОНЕТ?");
            texts.Add("mm_coins_body_text", "Купи в банке или получи за просмотр видео!");
            texts.Add("mm_coins_play_btn_text", "Free");
            texts.Add("mm_coins_bank_btn_text", "Банк");
            texts.Add("mm_coins_add_btn_text", "Нужно еще?");
            texts.Add("mm_energy_header_text", "УПС!");
            texts.Add("mm_energy_body_text", "Подожди пока они восстановятся или купи или посмотри видео.");
            texts.Add("mm_stars_header_text", "У ВАС НЕДОСТАТОЧНО ЗВЕЗД...");
            texts.Add("mm_stars_body_text", "Получай звзеды \nв мини - играх или в сканировании!");
            texts.Add("mm_stars_play_btn_text", "Play");
            texts.Add("mm_regard_coins_header_text", "Награда");
            texts.Add("mm_regard_coins_btn_text", "Забрать!");
            texts.Add("mm_regard_hearts_header_text", "Награда");
            texts.Add("mm_regard_hearts_btn_text", "Забрать!");
            texts.Add("mm_customizer_header_text", "Поменять");
            texts.Add("mm_customize_tutor_text", "Дважды быстро нажмите на предмет,который хотите поменять.");
            texts.Add("mm_namepanel_title_text", "Познакомимся?");
            texts.Add("mm_namepanel_placeholder_text", "Как вас зовут?...");
            texts.Add("mm_rate_rate_title_text", "Ну? Как?");
            texts.Add("mm_rate_rate_body_text", "Пожалуйста, оцените игру про котика.");
            texts.Add("mm_rate_rate_fail_body_text", "Спасибо! Ваше мнение очень важно для нас!");
            texts.Add("mm_rate_market_title_text", "Может быть...");
            texts.Add("mm_rate_market_body_text", "Напишете отзыв в Play Market?");
            texts.Add("mm_rate_market_btn_text", "Написать отзыв!");
            texts.Add("mm_sn_title_text", "Развлекушки!");
            texts.Add("mm_sn_body_text", "Хотите получать последние новости первым? Постить фоточки своих котов и участвовать в конкурсах хотите? Тогда подпишитесь!");
            texts.Add("mm_sn_no_btn_text", "Нет");
            texts.Add("mm_sn_later_btn_text", "Потом");
            texts.Add("mm_sn_subs_btn_text", "Подпись!");
            texts.Add("Main_StarsScreen_header_text", "НЕ ХВАТАЕТ ЗВЕЗД...!");
            texts.Add("Main_StarsScreen_title1_text", "Need more stars?");
            texts.Add("Main_StarsScreen_title_game_text", "Play \nGames");
            texts.Add("Main_StarsScreen_title_stars_text", "Earn \nStars");
            texts.Add("Main_StarsScreen_title_tasks_text", "Complete \ntasks!");
            texts.Add("Main_StarsScreen_btn_text_text", "Play!");
            texts.Add("Main_ScanTicketsScreen_header_text", "NEED SCAN TICKETS");
            texts.Add("Main_ScanTicketsScreen_title1_text", "Wanna scan?");
            texts.Add("Main_ScanTicketsScreen_title_tasks_text", "Complete \ntasks");
            texts.Add("Main_ScanTicketsScreen_title_scan_text", "Get scan \ntickets");
            texts.Add("Main_ScanTicketsScreen_title_rang_text", "Scan \nwhatever");
            texts.Add("Main_ScanTicketsScreen_btn_text_text", "Ok");


            //cs_catshow
            texts.Add("cs_catshow_header_text", "Кошачьи Выставки");
            texts.Add("cs_catshow_game_info_text", "Учавствуй в выставках, получай очки красоты и выигрывать призы.");
            texts.Add("cs_catshow_game_btn_text", "Играть");
            texts.Add("cs_catshow_rating_btn_text", "Рейтинг");
            texts.Add("cs_catshow_rating_prize_btn_text", "Призы по рейтингам");
            texts.Add("cs_catshow_ponts_prize_btn_text", "Призы по \nочкам");
            texts.Add("cs_catshow_pnts_points_text", "Очки");
            texts.Add("cs_catshow_pnts_prize_text", "Приз");
            texts.Add("cs_catshow_rt_place_text", "Место");
            texts.Add("cs_catshow_rt_name_text", "Имя");
            texts.Add("cs_catshow_rt_points_text", "Очки");
            texts.Add("cs_catshow_rtp_place_text", "Ваше \nместо в");
            texts.Add("cs_catshow_rtp_prize_text", "Приз");
            texts.Add("cs_catshow_end_show_text", "До результатов выставки осталось ");
            texts.Add("cs_catshow_end_show_day_text", " дней ");
            texts.Add("cs_catshow_end_show_hours_text", " часов ");
            texts.Add("cs_catshow_end_show_minutes_text", " минут ");
            texts.Add("cs_catshow_end_show_seconds_text", " секунд");

            //cs
            texts.Add("cs_coins_header_text", "НУЖНО БОЛЬШЕ МОНЕТ?");
            texts.Add("cs_coins_body_text", "Играй в игры или получи награду за просмотр видео!");
            texts.Add("cs_coins_add_btn_text", "Нужно еще?");
            texts.Add("cs_energy_header_text", "УПС!");
            texts.Add("cs_energy_body_text", "Подожди пока они восстановятся или купи или посмотри видео.");
            texts.Add("cs_energy_add_btn_text", "Нужно еще?");
            texts.Add("cs_energy_regard_header_text", "Награда");
            texts.Add("cs_energy_regard_btn_text", "Забрать!");

            //shop
            texts.Add("sc_shop_header_text", "Магазин");
            texts.Add("sc_shop_head_btn_text", "Головные уборы");
            texts.Add("sc_shop_collar_btn_text", "Ошейники");
            texts.Add("sc_shop_skeen_btn_text", "Расцветка");
            texts.Add("sc_shop_eye_btn_text", "Глазные линзы");
            texts.Add("sc_shop_glasses_btn_text", "Очки");

            //dialog
            texts.Add("dialog_next_btn_text", "Далее");
            texts.Add("dialog_mission_area_title_text", "Новая миссия");

            //prize
            texts.Add("prize_points_header_text", "Ух ты!");
            texts.Add("prize_points_points_text", "Ты получил");
            texts.Add("prize_points_prize_text", "Твой приз");
            texts.Add("prize_points_btn_text", "Получить!");
            texts.Add("prize_week_header_text", "Результаты Выставок");
            texts.Add("prize_week_points_text", "Собрано");
            texts.Add("prize_week_place_text", "Место");
            texts.Add("prize_week_percent_place_text", "Место в");
            texts.Add("prize_week_prize_text", "Выш приз");
            texts.Add("prize_week_btn_text", "Забрать!");
            texts.Add("mm_daily_prize_header", "Подарочки!");
            texts.Add("mm_daily_prize_day", "День");
            texts.Add("mm_daily_prize_button_open_text", "Открыть");
            texts.Add("mm_daily_prize_button_pick_up_text", "Забрать");
            texts.Add("mm_daily_prize_next_text", "Следующий подарок появится через %N% часов");

            other_strings.Add("mm_sn_icon_name", "icon_SN_RU");
            other_strings.Add("mm_rate_url", "https://play.google.com/store/apps/details?id=com.scannerapps.whatCat&hl=ru");
            other_strings.Add("social_url", "https://vk.com/happy_games_app");

            //boosters
            texts.Add("booster_reborn_description", "Можно будет продолжить играть даже если упадешь!");
            texts.Add("booster_supercat_description", "Пролетает без препятствий %N% метров!");
            texts.Add("booster_magnet_description", "Притягивет все монетки рядом с котиком.");
            texts.Add("booster_reborn_name", "Кошачья жизнь");
            texts.Add("booster_supercat_name", "Суперкот");
            texts.Add("booster_magnet_name", "Магнит Банкира");
            texts.Add("booster_reborn_upgrate_description", "%N% раз за игру");
            texts.Add("booster_magnet_upgrate_description", "%N% секунд");
            texts.Add("booster_supercat_upgrate_description", "%N% метров");

            //push
            texts.Add("push_locale_text", "ru");
            texts.Add("push_1_text", "Котики приготовили подарок! Приходи забрать его!");
            texts.Add("push_2_text", "Пожалуйста, помоги котикам. В доме бабушки еще много работы.");
            texts.Add("push_3_text", "Пора вернуться и заработать парочку звезд!");
            texts.Add("push_4_text", "Уже неделя как мы знакомы. Ура ;)");
            texts.Add("push_5_text", "Котики приготовили подарок! Приходи забрать его!");
            texts.Add("push_6_text", "Пожалуйста, помоги котикам. В доме бабушки еще много работы.");
            texts.Add("push_7_text", "Пора вернуться и заработать парочку звезд!");
            texts.Add("push_8_text", "Доставка подарков не дремлет! Заходи, забирай!");
            texts.Add("push_9_text", "Котики приготовили подарок! Приходи забрать его!");
            texts.Add("push_10_text", "Пожалуйста, помоги котикам. В доме бабушки еще много работы.");
            texts.Add("push_11_text", "Пора вернуться и заработать парочку звезд!");
            texts.Add("push_12_text", "	Доставка подарков не дремлет! Заходи, забирай!");
            texts.Add("push_13_text", "Котики приготовили подарок! Приходи забрать его!");
            texts.Add("push_14_text", "Пожалуйста, помоги котикам. В доме бабушки еще много работы.");
            texts.Add("push_15_text", "Пора вернуться и заработать парочку звезд!");
            texts.Add("push_16_text", "Я так рад, что месяц назад мы встретились!");

            //bubble
            texts.Add("bubble_WON_MINIGAME_0", "ты так круто справляешься с игрой!");
            texts.Add("bubble_WON_MINIGAME_1", "Победитель во всем!");
            texts.Add("bubble_WON_MINIGAME_2", "Я восхищаюсь твоими способностями!");
            texts.Add("bubble_WON_MINIGAME_3", "Расскажи как быть таким крутаном?");
            texts.Add("bubble_WON_MINIGAME_4", "Я не сомневался, что ты победишь!");
            texts.Add("bubble_WON_MINIGAME_5", "Вы видели? Это было круто!");
            texts.Add("bubble_WON_MINIGAME_6", "Еще одна звездочка в твою копилку!");
            texts.Add("bubble_WON_MINIGAME_7", "Не знаю есть ли кто-то круче тебя...");
            texts.Add("bubble_WON_MINIGAME_8", "Всегда мечтал о таком друге, как ты!");
            texts.Add("bubble_WON_MINIGAME_9", "Как?! Как ты с этим справляешься?");

            texts.Add("bubble_LOSE_MINIGAME_0", "Мы верим в тебя, не расстраивайся.");
            texts.Add("bubble_LOSE_MINIGAME_1", "Ты справишься со всеми неудачами!");
            texts.Add("bubble_LOSE_MINIGAME_2", "Главное не оставлять попыток и все получится!");
            texts.Add("bubble_LOSE_MINIGAME_3", "Ты все равно круче всех!");
            texts.Add("bubble_LOSE_MINIGAME_4", "Это не последняя попытка, ты молодец!");
            texts.Add("bubble_LOSE_MINIGAME_5", "Сам я и 5 секунд не могу в игре продержаться!");
            texts.Add("bubble_LOSE_MINIGAME_6", "Не грусти, я не знаю тех кто прошел его с первого раза.");
            texts.Add("bubble_LOSE_MINIGAME_7", "Ну ничего! Приготовимся к следующей попытке!");
            texts.Add("bubble_LOSE_MINIGAME_8", "Не унывай, в следующий раз получится!");
            texts.Add("bubble_LOSE_MINIGAME_9", "Будь выше этого!Победа не за горами!");

            texts.Add("bubble_SCANNED_0", "Уау!Такие смешные сканирования!");
            texts.Add("bubble_SCANNED_1", "Эх, иногда хочется камеру покруче...");
            texts.Add("bubble_SCANNED_2", "Жалко у нас нету теста: \"какой ты человек ? \" :)");
            texts.Add("bubble_SCANNED_3", "Какой ты кот? Ты классный кот, вот и все!");
            texts.Add("bubble_SCANNED_4", "Иногда смотрю на тебя и вижу котика.");
            texts.Add("bubble_SCANNED_5", "Чем больше сканируешь, тем круче знаешь котов.");
            texts.Add("bubble_SCANNED_6", "Ты очень смешной! :)");
            texts.Add("bubble_SCANNED_7", "Веселые котята похожи на тебя!");

            texts.Add("bubble_NEW_RANK_REACHED_0", "Поздравляю с новым рангом!");

            texts.Add("bubble_tap_Main_0", "Котики любят спать. 70 % суток уходит на сон.");
            texts.Add("bubble_tap_Main_1", "Коты умеют создавать около 100 звуков.");
            texts.Add("bubble_tap_Main_2", "В мире около 40 различных пород домашних кошек.");
            texts.Add("bubble_tap_Main_3", "Превую домашнюю кошку нашли на Кипре");
            texts.Add("bubble_tap_Main_4", "Первая кошка астронавт — Felicette.Француженка.");
            texts.Add("bubble_tap_Main_5", "Домашние кошки могут разгоняться до 50 км / ч.");
            texts.Add("bubble_tap_Main_6", "Коты умеют прыгать в 5 раз выше своего роста.");
            texts.Add("bubble_tap_Main_7", "Ученые не знают как мы мурлычем.");
            texts.Add("bubble_tap_Main_8", "У кошек приблизительно 24 уса.");
            texts.Add("bubble_tap_Main_9", "Коты круто видят в темноте.");
            texts.Add("bubble_tap_Main_10", "Кошки умеют поворачивать уши на 180 градусов.");
            texts.Add("bubble_tap_Main_11", "У взрослой кошки 30 зубов.У котят — 26.");
            texts.Add("bubble_tap_Main_12", "Коты умеют пить морскую воду.");
            texts.Add("bubble_tap_Main_13", "Так приятно, что мы все теперь в одном доме.");
            texts.Add("bubble_tap_Main_14", "Как много ты знаешь о котиках?");
            texts.Add("bubble_tap_Main_15", "Так радостно видеть друзей рядом.");
            texts.Add("bubble_tap_Main_16", "мур - мур");
            texts.Add("bubble_tap_Main_17", "мяу - мяу");
            texts.Add("bubble_tap_Main_18", "Мы будем еще что-то строить?");
            texts.Add("bubble_tap_Main_19", "Я люблю гулять у пруда.");
            texts.Add("bubble_tap_Main_20", "Боюсь возвращаться в город один.");
            texts.Add("bubble_tap_Main_21", "Иногда смотрю вокруг и радуюсь.");
            texts.Add("bubble_tap_Main_22", "Я не люблю купаться.");
            texts.Add("bubble_tap_Main_23", "Один раз я спал на голове человека.");
            texts.Add("bubble_tap_Main_24", "Я люблю царапать обои.");
            texts.Add("bubble_tap_Main_25", "Иногда хочется путешествовать.");
            texts.Add("bubble_tap_Main_26", "Люблю валяться на солнышке.");
            texts.Add("bubble_tap_Main_27", "Когда гладят — это так приятно.Муррр.");

            texts.Add("bubble_tap_Jakky_0", "Я Джекки острый коготок.");
            texts.Add("bubble_tap_Jakky_1", "Котики любят спать. 70 % суток уходит на сон.");
            texts.Add("bubble_tap_Jakky_2", "Никто не разбирается в дверях лучше меня");
            texts.Add("bubble_tap_Jakky_3", "Я могу попасть туда где никого не было.");
            texts.Add("bubble_tap_Jakky_4", "Некоторые думают, что я никогда не был у них дома.");

            texts.Add("bubble_tap_Black_0", "Так круто, что ты с нами.");
            texts.Add("bubble_tap_Black_1", "Я очень рад, что мы строим кошачий дом.");
            texts.Add("bubble_tap_Black_2", "Иногда котики рассказывают про себя факты.");
            texts.Add("bubble_tap_Black_3", "Свой дом!Я вообще в шоке:)");
            texts.Add("bubble_tap_Black_4", "Я умею гоняться за своим хвостом. А ты?");
            texts.Add("bubble_tap_Black_5", "Я горжусь, что помогаю вам в строительстве.");
            texts.Add("bubble_tap_Black_6", "У меня много знакомых в местной округе.");
            texts.Add("bubble_tap_Black_7", "Когда я был маленьким, я был чернее.");

            texts.Add("bubble_customize_0", "Стало сразу уютнее!Хочу тут полежать");
            texts.Add("bubble_customize_1", "Я ждал именно этого.");
            texts.Add("bubble_customize_2", "Так приятно, что ты заботишься о нас.");
            texts.Add("bubble_customize_3", "Уау!Спасибо!");
            texts.Add("bubble_customize_4", "Без тебя было бы так скучно!");
            texts.Add("bubble_customize_5", "Ты самый лучший дизайнер!");
            texts.Add("bubble_customize_6", "Ты умеешь удивить!");
            texts.Add("bubble_customize_7", "О!мне очень нравится!");
            texts.Add("bubble_customize_8", "Обожаю прикольные перемены!");
            texts.Add("bubble_customize_9", "Очень круто!Спасибо!");

            texts.Add("bubble_relax", "Тут всегда можно будет отдохнуть.");
            texts.Add("bubble_books", "Тяга к знаниям — это про меня:)");
            texts.Add("bubble_joy", "Так радует глаз! Придется спрятать коготки!");
            texts.Add("bubble_game", "Ура! Можно будет играть сколько захочешь!");
            texts.Add("bubble_soft_floor", "Как приятно моим лапкам!");

            texts.Add("bubble_money_0", "Можно посмотреть видео и тогда монеток прибавится.");
            texts.Add("bubble_money_1", "Я люблю бегать просто так в игре на монетки.");
            texts.Add("bubble_money_2", "Чем больше монеток, тем красивее наш дом.");
            texts.Add("bubble_money_3", "Если заходишь в игру каждый день, то дарят подарки. я люблю подарки.");

            texts.Add("bubble_no_speedup_0", "Так надоело ждать? Мужет ускорим?");
            texts.Add("bubble_no_speedup_1", "Ускорение вроде бы не дорогое. Можно заработать.");
            texts.Add("bubble_no_speedup_2", "Иногда, я смотрю на этот таймер и засыпаю.");
            texts.Add("bubble_no_speedup_3", "Эх.Надоело ждать. Когда там таймер закончится?");
            texts.Add("bubble_no_speedup_4", "Когда это время уже закончится.");

            texts.Add("bubble_no_customize_0", "Тебе не наскучили стены?");
            texts.Add("bubble_no_customize_1", "Хочется поменять кухню.");
            texts.Add("bubble_no_customize_2", "Я бы попробовал новый пол.");
            texts.Add("bubble_no_customize_3", "Кажется мы попробовали не все диваны.");
            texts.Add("bubble_no_customize_4", "Давай поработаем над интерьером?");

            //bank
            texts.Add("Main_Bank_restore_purchases_btn_text_text", "Восстановить покупки");
            texts.Add("Main_Bank_header_text", "Банк");

            //tm
            texts.Add("pp_tutor", "Нажмите 3 раза на продукт, что бы оформить заказ.");
            texts.Add("customer_tutor", "Нажмите на покупателя, что бы продать ему товар.");
            texts.Add("pu_tutor", "Нажмите на товар, что бы запустить производство.");
            texts.Add("success_customers_goal_tutor", "В этом уровне у вас должно быть не меньше 12 довольных клиентов!");
            texts.Add("all_customers_success_goal_tutor", "В этом уровне ни один клиент не уйдёт не довольным!");
        }            
    }

    public static string getOtherText(string key)
    {
        return other_strings[key];
    }

    public static string getCatsText(string key)
    {
        return pets[key];
    }

    public static string getTaskName(int key)
    {
        return task_names[key.ToString()];
    }

    public static bool getLocalString(string key, out string result)
    {
        string value;
        if (texts.TryGetValue(key, out value))
        {
            result = value;
            return true;
        }
        else
        {
            result = "";
            return false;
        }
    }

    public static string getText(string key)
    {
        return texts[key];
    }

    public static string getDialogsText(int task, int dialog)
    {
        return dialogs["task" + task.ToString() + "_" + dialog.ToString()];//.Replace("%USERNAME%", DataController.instance.catsPurse.Name);
    }
}
