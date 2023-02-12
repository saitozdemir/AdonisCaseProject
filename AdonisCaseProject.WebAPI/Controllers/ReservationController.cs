using AdonisCaseProject.Class;
using AdonisCaseProject.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace AdonisCaseProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        [HttpGet("GetReservationList", Name = "GetReservationList")]
        public IActionResult GetReservationList()
        {
            DataService db = new DataService();
            IEnumerable<Reservation> list = db.ReturnDataAsIE<Reservation>("CaseProject_sp_GetReservationList", CommandType.StoredProcedure);          
            return Ok(list);
        }


        [HttpPost("CreateReservation", Name = "CreateReservation")]
        public IActionResult CreateReservation(Reservation reservation)
        {
            DataService db = new DataService();
            db.AddParameter("@GuestName", reservation.GuestName, DbType.String);
            db.AddParameter("@DepartureDate", reservation.DepartureDate, DbType.DateTime);
            db.AddParameter("@ArrivalDate", reservation.ArrivalDate ,DbType.DateTime);
            db.AddParameter("@Status", reservation.Status,DbType.Boolean);

            db.DataCommit("CaseProject_sp_CreateReservation", CommandType.StoredProcedure);
            return Ok();
        }

        [HttpPost("DeleteReservation", Name = "DeleteReservation")]
        public IActionResult DeleteReservation(int ReservationID)
        {
            DataService db = new DataService();
            db.AddParameter("@ReservationID", ReservationID, DbType.Int64);
            db.DataCommit("CaseProject_sp_DeleteReservation", CommandType.StoredProcedure);
            return Ok();
        }   

        [HttpGet]
        public IActionResult Get(int Id)
        {
            DataService db = new DataService();
            db.AddParameter("@ReservationId", Id, DbType.Int32);
            Reservation reservation = new Reservation();
            reservation = db.GetQuestionbyId();

            return Ok(reservation);
        }


        [HttpPost("UpdateReservation", Name = "UpdateReservation")]
        public IActionResult Update(Reservation reservation)
        {
            DataService db = new DataService();
            db.AddParameter("@ReservationID", reservation.ReservationId, DbType.Int64);
            db.AddParameter("@GuestName", reservation.GuestName, DbType.String);
            db.AddParameter("@DepartureDate", reservation.DepartureDate, DbType.DateTime);
            db.AddParameter("@ArrivalDate", reservation.ArrivalDate, DbType.DateTime);
            db.AddParameter("@Status", reservation.Status, DbType.Boolean);
            db.DataCommit("CaseProject_sp_UpdateReservationByID", CommandType.StoredProcedure);


            return Ok();
        }
    }
}

