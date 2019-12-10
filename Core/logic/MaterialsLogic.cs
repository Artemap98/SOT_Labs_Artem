namespace Core
{
    public class MaterialsLogic : AbstractLogic
    {
        public MaterialsLogic(AbstractConnection c) : base(c)
        {
        }

        public myDataBaseDataSet GetMaterials()
        {
            return Cache;
        }

        public myDataBaseDataSet GetMaterialsDirectlyFromDb()
        {
            return getRecordsDirectlyFromDb();
        }
        public bool AddMaterial(myDataBaseDataSet.MaterialsRow row)
        {
            return AddRecord(row);
        }

        public myDataBaseDataSet.MaterialsRow NewMaterialsRow()
        {
            return (myDataBaseDataSet.MaterialsRow) NewRow();
        }

        public bool DeleteMaterialWithID(int id)
        {
            //getMaterialByID(id).Delete();
            //return true;
            return DeleteRecordWithId(id);
        }

        public myDataBaseDataSet.MaterialsRow getMaterialByID(int id)
        {
            //return Cache.Materials.FindByMaterial_id(id);
            return (myDataBaseDataSet.MaterialsRow) getRecordWithId(id);
        }

        public bool UpdateMaterial(int id, myDataBaseDataSet.MaterialsRow row)
        {
            /*myDataBaseDataSet.MaterialsRow oldRow = getMaterialByID(id);
            if (!row.IsNull("name"))
                oldRow.name = row.name;
            if (!row.IsNull("Staff_id"))
                oldRow.Staff_id = row.Staff_id;
            return true;*/
            return UpdateRecord(id, row);
        }

        protected override AbstractDataAccessor provideWithAccessor()
        {
            return new MaterialsAccessor();
        }
    }
}
