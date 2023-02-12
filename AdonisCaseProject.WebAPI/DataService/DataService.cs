using AdonisCaseProject.WebAPI.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AdonisCaseProject.Class
{
    public class DataService
    {
        IDbConnection db;
        IDbTransaction transaction;
        DynamicParameters param;
        protected bool isBeginedConnection = false;


        public DataService()
        {
            db = new SqlConnection("server=DESKTOP-OKME90P\\SQLEXPRESS; Database=CaseProject; integrated security=true;");
            param = new DynamicParameters();
        }


        #region Function

        protected void ConnState()
        {
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
                isBeginedConnection = true;
            }
        }

        public void CloseConn()
        {
            if (db.State == ConnectionState.Open)
            {
                db.Close();
                isBeginedConnection = false;
            }
        }

        public void BeginTransaction()
        {
            ConnState();

            transaction = db.BeginTransaction();
        }

        private IDbTransaction IfHaveTransactionReturn()
        {
            if (transaction != null)
                return transaction;
            else
                return null;
        }

        public void CommitTransaction()
        {
            transaction.Commit();
            transaction = null;
        }

        public void RollBackTransaction()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction = null;
            }

        }

        #endregion Function

        #region DynamicObject
        public void AddParameter(string variable, object data)
        {
            AddParameter(variable, data, DbType.String);
        }

        public void AddParameter(string variable, object data, DbType dbType)
        {
            AddParameter(variable, data, dbType, ParameterDirection.Input);
        }

        public void AddParameter(string variable, object data, DbType dbType, ParameterDirection parameterDirection)
        {
            param.Add(variable, data, dbType, parameterDirection);
        }

        public void DataCommit(string query)
        {
            DataCommit(query, CommandType.Text);
        }

        public void DataCommit(string query, CommandType cmdType)
        {
            ConnState();

            if (param.ParameterNames.Count() > 0) DataCommitWithParam(query, cmdType);
            else DataCommiNottWithParam(query, cmdType);

            CloseConn();

            param = new DynamicParameters();
        }

        private void DataCommiNottWithParam(string query, CommandType cmdType)
        {
            db.Execute(query, commandType: cmdType, transaction: IfHaveTransactionReturn());
        }

        private void DataCommitWithParam(string query, CommandType cmdType)
        {
            db.Execute(query, param, commandType: cmdType, transaction: IfHaveTransactionReturn());
        }

        public IDataReader GetDataReader(string query)
        {
            return GetDataReader(query, CommandType.Text);
        }

        public IDataReader GetDataReader(string query, CommandType cmdType)
        {
            ConnState();

            IDataReader dr;

            if (param.ParameterNames.Count() > 0) dr = GetDataReaderWithParam(query, cmdType);
            else dr = GetDataReaderNotWithParam(query, cmdType);

            param = new DynamicParameters();

            return dr;
        }

        private IDataReader GetDataReaderNotWithParam(string query, CommandType cmdType)
        {
            return db.ExecuteReader(query, commandType: cmdType);
        }

        private IDataReader GetDataReaderWithParam(string query, CommandType cmdType)
        {
            return db.ExecuteReader(query, param, commandType: cmdType);
        }

        public object ReturnScalerData(string query)
        {
            return ReturnScalerData(query, CommandType.Text);
        }

        public object ReturnScalerData(string query, CommandType cmdType)
        {
            object obj;
            ConnState();

            if (param.ParameterNames.Any())
                obj = ReturnScalerDataWithParam(query, cmdType);
            else
                obj = ReturnScalerDataNotWithParam(query, cmdType);

            if (IfHaveTransactionReturn() == null && isBeginedConnection == false)
                CloseConn();

            param = new DynamicParameters();
            return obj;
        }


        private object ReturnScalerDataNotWithParam(string query, CommandType cmdType)
        {
            return db.ExecuteScalar(query, commandType: cmdType, transaction: IfHaveTransactionReturn());
        }

        private object ReturnScalerDataWithParam(string query, CommandType cmdType)
        {
            return db.ExecuteScalar(query, param, commandType: cmdType, transaction: IfHaveTransactionReturn());
        }


        public IEnumerable<T> ReturnDataAsIE<T>(string query, CommandType cmdtType)
        {
            ConnState();

            IEnumerable<T> ls = db.Query<T>(query, param: param, transaction: IfHaveTransactionReturn(), commandType: cmdtType);

            param = new DynamicParameters();
            if (IfHaveTransactionReturn() == null && isBeginedConnection == false)

                CloseConn();
            return ls;
        }


        public T ReturnData<T>(string query, CommandType cmdtType)
        {
            ConnState();

            T ls = db.QueryFirstOrDefault<T>(query, param: param, transaction: IfHaveTransactionReturn(), commandType: cmdtType);

            param = new DynamicParameters();
            if (IfHaveTransactionReturn() == null && isBeginedConnection == false)

                CloseConn();
            return ls;
        }

        public Reservation GetQuestionbyId()
        {
            ConnState();
            return db.QueryFirstOrDefault<Reservation>("SELECT * FROM Reservation WHERE ReservationId=@ReservationID", param);
        }
    }
}


#endregion