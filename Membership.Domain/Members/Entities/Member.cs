using System;
using System.Collections.Generic;
using System.Text;
using Kernel.Domain.Primitives;
using ErrorOr;

namespace Membership.Domain.Members.Entities
{
    public class Member : Aggregate<MemberId> 
    {
        public const int firstNameMaxChars = 50;
        public const int lastNameMaxChars = 50;
        public const int middleNameMaxLength = 50;
       
        



        public string FirstName { get; private set; }

        public string? MiddleName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string FullName => $"{LastName}, {FirstName}";

        public DateOnly DateOfBirth { get; private set; }

        public DateTime RegistrationDate {  get; private set; }

        public bool IsActive { get; private set; }

        public Guid HistoryActionId { get; private set; }  


        protected Member() {

        }

        private Member(string firstName,
            string? middleName, 
            string lastName,
            string email, 
            DateOnly dateOfBirth,
            Guid? historyActionId = null)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            RegistrationDate = DateTime.UtcNow;
            IsActive = true;
            HistoryActionId = historyActionId ?? Guid.NewGuid();
            
        }

        public static ErrorOr<Member> CreateMember(
            string firstName,
            string? middleName,
            string lastName, 
            string email, 
            DateOnly dateOfBirth
            )
        {
            var results = CheckInvariants(firstName, middleName, lastName,email, dateOfBirth);
            
            if (results.Count > 0)
            {
                return results;
            }


            var member = new Member(
                firstName,
                middleName,
                lastName,
                email,
                dateOfBirth,
                Guid.NewGuid());



            return member;
        }


        public ErrorOr<Success> UpdateMember(
            string firstName,
            string middleName,
            string lastName,
            string email,
            DateOnly dateOfBirth)
        {
            var results = CheckInvariants(firstName, middleName, lastName, email, dateOfBirth);

            if (results.Count > 0)
            {
                return results;
            }

            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;

            return Result.Success;
        }

        private static List<Error> CheckInvariants(
            string firstName,
            string? middleName,
            string lastName,
            string email,
            DateOnly? dateOfBirth
            
            )

        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(firstName))
            {
                errors.Add(Error.Validation(
                    code: "Member.FirstName",
                    description: "First name is required"));
            }


            if (firstName.Length > firstNameMaxChars)
            {
                errors.Add(Error.Validation(
                    description: $"error.memberFirstNameTooLong"));
            }

            if (middleName?.Length > middleNameMaxLength)
            {
                errors.Add(Error.Validation(
                    description: $"error.memberMiddleNameTooLong"));
            }

            if(string.IsNullOrWhiteSpace(lastName))
            {
                errors.Add(Error.Validation(
                    code: "Member.LastName",
                    description: "Last name is required"));
            }


            if (lastName.Length > lastNameMaxChars)
            {
                errors.Add(Error.Validation(
                    description: $"error.memberLastNameTooLong"));

            }
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(Error.Validation(
                    code: "Member.Email",
                    description: "Email is required"));
            }

            if (dateOfBirth.HasValue && dateOfBirth.Value >= DateOnly.FromDateTime(DateTime.UtcNow))
            {
                errors.Add(Error.Validation(
                    description: $"error.memberDateOfBirthInvalid"));
            }

            return errors;
        }

    }
}
