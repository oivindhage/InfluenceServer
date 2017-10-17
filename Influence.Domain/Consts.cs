namespace Influence.Domain
{
    public static class Consts
    {
        public static class GamePhase
        {
            public const string NotStarted = "Not started";
            public const string Ongoing = "Ongoing";
            public const string Finished = "Finished";
        }

        public static class PlayerPhase
        {
            public const string Undefined = "Undefined";
            public const string MoveAndAttack = "MoveAndAttack";
            public const string Reinforce = "Reinforce";
        }
    }
}