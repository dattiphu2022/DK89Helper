using System;

namespace DK89.DomainBase
{
    /// <summary>
    /// Result pattern that gives code flexibility in catching successful outcome or failure error.
    /// </summary>
    /// <typeparam name="T">The returning type.</typeparam>
    public class Result<T> : IResult<T>
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Indicates whether the operation failed.
        /// This is the opposite of IsSuccess.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Contains the result value if the operation was successful.
        /// If the operation failed, this will be null.
        /// </summary>
        public T? Value { get; }

        /// <summary>
        /// Contains the error message if the operation failed.
        /// If the operation was successful, this will be null.
        /// </summary>
        public string? ErrorCode { get; }
        public string? ErrorMessage { get; }

        /// <summary>
        /// Private constructor to enforce valid Result creation.
        /// Ensures that a result cannot have both a value and an error.
        /// </summary>
        /// <param name="value">The result value (valid only if isSuccess is true).</param>
        /// <param name="isSuccess">Indicates whether the operation was successful.</param>
        /// <param name="errorCode">The error message (valid only if isSuccess is false).</param>
        protected Result(T? value, bool isSuccess, string? errorCode, string? errorMessage)
        {
            // Ensures that a successful result does not have an error message.
            if (isSuccess)
            {
                if (errorCode is not null | errorMessage is not null)
                {
                    throw new InvalidOperationException("Cannot have an error when success.");
                }
            }

            // Ensures that a failed result does not have a value.
            if (!isSuccess && value != null)
                throw new InvalidOperationException("Cannot have a value when failure.");

            IsSuccess = isSuccess;
            Value = value;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Creates a successful result containing a value.
        /// </summary>
        /// <param name="value">The result value.</param>
        /// <returns>A successful Result object.</returns>
        public static Result<T> Success(T value)
        {
            return new Result<T>(
                value: value,
                isSuccess: true,
                errorCode: null,
                errorMessage: null);
        }

        /// <summary>
        /// Creates a failed result with an error message.
        /// </summary>
        /// <param name="errorCode">The error message describing the failure's code.</param>
        /// <param name="errorMessage">The error message describing the failure's message.</param>
        /// <returns>A failed Result object.</returns>
        public static Result<T> Failure(string errorCode, string errorMessage)
        {
            return new(
            value: default,
            isSuccess: false,
            errorCode: errorCode,
            errorMessage: errorMessage);
        }
    }
}
