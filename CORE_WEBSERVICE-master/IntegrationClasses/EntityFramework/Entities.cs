using Integration.EntityFramework;

namespace Integration.Code
{
    public static class Entities
    {
        #region Private Atributes
        private static readonly IntegrationDBEntities integration = new IntegrationDBEntities();
        private static readonly accountTable accountTable = new accountTable();
        private static readonly clientTable clientTable = new clientTable();
        #endregion

        #region Encapsulation
        public static IntegrationDBEntities Integration => integration;

        public static accountTable AccountTable => accountTable;

        public static clientTable ClientTable => clientTable;
        #endregion
    }
}
