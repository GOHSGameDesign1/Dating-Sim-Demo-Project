
-> Main

==Main==
Ueeghhhhh...... #speaker: You #splash:Black
(I struggle to open my eyes as my body aches all over.) #playSound:Rustle
(I can't move...)
(Am I tied to a chair?)
(What Happened?)
(All I remember is returning home from work...)
(The rest I can't remember...)
Well Well Well... #speaker: ???
Look who finally decided to wake up...
W-what? #speaker:You
You're...
A TALKING FRIDGE?!?!?!? #splash:None #switchTheme:Frigid
Surprised? #speaker: ???
WHA- BUT.. HOW-#speaker:You
...
I'm dreaming aren't I...
Dreaming? #speaker:???
Oh ho ho ho!#expression: Fridge_Laughing
I assure you dear, this is no dream.
Here's the deal.#expression: Fridge_Idle
My Name is Frida. #speaker:Frida
I've become quite tired with how you humans treat us fridges.
You use without any regard or respect, And when we can't perform any longer... You dispose of us like it's nothing!
So, to exact my revenge on mankind, I thought I'd start by kidnapping and disposing of YOU.
Wait... You don't mean... #speaker:You #switchTheme:none
MWAHAHAHAHAHAHAHA!#speaker:Frida #expression:Fridge_Knife #switchTheme:Murder
Eep!#speaker:You
(I need to think of something fast!)
    + [Threaten]
        Oh yeah? Bring it on you stupid fridge! I'm not afraid of you! #switchTheme:none
        I don't think you're in much position to talk when your all tied up dear.#speaker:Frida
        Oh well then!
        -> Death
    + [Try Stalling]
        Wait wait wait! Don't murder me yet!
        And why should I stop? #speaker:Frida
        I'm your first victim right?#speaker: You
        That's a HUGE milestone.
        Shouldn't we get to know each other a bit first?
        Hmmmm...#speaker:Frida #switchTheme:none
        I suppose you're right.
        It would be a shame to have my first kill be so impersonal.
        So then...
        -> Main2

==Main2==
What would like to know about me?#speaker:Frida #switchTheme:Lofi #expression:Fridge_Question
    * [How are you talking?]
        So... How are you talking exactly?#speaker:You
        All of fridge-kind can talk, we just don't like to most of the time.#speaker:Frida
        Does that mean other inanimate objects can talk?#speaker:You
        Like... Toilets?
        Oh ho ho ho!!!#speaker:Frida #expression:Fridge_Laughing
        Of course not! That would be ridiculous!
        -> Main2
    * [Why do you hate mankind?]
        Why do you hate mankind so much?#speaker:You
    * Ya Like Jazz?#speaker:You
        of course I do.#speaker:Frida
        Oh.#speaker:You
        Cool.
        ->Main2
- Didn't I already tell you?#speaker:Frida  #expression:Fridge_Idle
You humans always leave us our doors slightly open, stuff us with rotten leftovers no one eats, and the worst part is...
When we stop working... You throw us out like we never even mattered!
    + [Console]
        -> Consoling
    + [Provoke]
        Skill issue?#speaker:You #switchTheme:none
        WHA- #speaker:Frida #expression:Fridge_Exclaim
        And here I was thinking you were somehow different from him... #expression:Fridge_Idle
        Thank you for confirming my views on mankind.
        -> Death


==Consoling==
Hey now! I'm sure we're not all that bad!#speaker:You
What makes you say that?#speaker:Frida
I can't say for absolutely everyone but I know for a fact that most people hate it when their fridge stops working. #speaker:You
You guys mean a lot to us.
If thats the case... #speaker:Frida #switchTheme:none
Then why didn't my owner care about me?
Frida... #speaker:You
You mean that you were treated like that by your previous owner?
... #speaker:Frida
Yes...
It's ok... #speaker:You
    + I could treat you right~ #switchTheme:Piano
        W-what? #speaker:Frida #expression: Fridge_Blushing
    + He's an awful person!
        No one should be treated like that! #switchTheme:Piano
        Yeah!#speaker:Frida #expression:Fridge_Exclaim
- Wait.. #expression: Fridge_Idle
No! Im supposed to kill you!
I'm supposed to exact my revenge on mankind!
    +The only thing exact here is your beauty~ #speaker:You
        ........#speaker:Frida #expression:Fridge_Blushing
    +Revenge isn't the only option! #speaker:You
        ...#speaker:Frida #expression:Fridge_Idle
-The truth is...#speaker:Frida
I don't want to kill you...
But I can't go back to being a normal fridge again.
What do I do? #expression:Fridge_Question
    +Go out with me#speaker:You
        R-really??? #speaker:Frida #expression:Fridge_Blushing
        ....
        I would like that...
    +Be my friend#speaker:You #expression:Fridge_Idle
-What do you say we stop by for a coffee? #speaker:You
Sure! #speaker:Frida
After you get me untied that is...#speaker:You
Oh! Sorry about that! #speaker:Frida #expression:Fridge_Exclaim
->HAPPY_END

==HAPPY_END==
And that is the story of the most amazing fridge I've ever met. #speaker:You #splash:Black
THE END #speaker:NONE
-> END

==Death==
YOU DIED#speaker:NONE #splash:Black #playSound:Stab #switchTheme:Frigid
-> END