using MediatR;
using Reservation.Application.Rooms.DTOs;
using ErrorOr;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Reservation.Application.Rooms.Queries
{
    public class SearchRoomQueryHandler : IRequestHandler <GetRoomQuery, ErrorOr<IReadOnlyList<RoomSearchResultDto>>>
    {
        private readonly IDbConnection _dbConnection;

        public SearchRoomQueryHandler (IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

        }

        public async Task <ErrorOr<IReadOnlyList<RoomSearchResultDto>>> Handle (
            GetRoomQuery query, 
            CancellationToken Ct
            )
        {


            var parameters = new DynamicParameters();
            var conditions = new List<string>();

            if (query.FloorNumber.HasValue)
            {
                conditions.Add("r.Floor_Number = @FloorNumber");
                parameters.Add("FloorNumber", query.FloorNumber.Value);
            }

            if (!string.IsNullOrWhiteSpace(query.RoomType))
            {
                conditions.Add("r.RoomType_Category = @RoomType");
                parameters.Add("RoomType", query.RoomType.Trim());
            }

            if (query.ActiveOnly)
            {
                conditions.Add("r.IsActive = 1");
            }

            var whereClause = conditions.Count > 0
                ? "WHERE " + string.Join(" AND ", conditions)
                : string.Empty;

            var sql = $"""
            SELECT
                r.Id,
                r.RoomNumber,
                r.Floor_Number AS FloorNumber,
                r.RoomType_Category AS RoomType,
                r.IsActive
            FROM Reservation.Rooms r
            {whereClause}
            ORDER BY r.Floor_Number, r.RoomNumber;
            """;

            var rooms = await _dbConnection.QueryAsync<RoomSearchResultDto>(
                sql,
                parameters);

            return rooms.ToList();





        }



    }
}
