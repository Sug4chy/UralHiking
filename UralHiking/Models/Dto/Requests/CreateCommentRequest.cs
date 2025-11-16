using System.ComponentModel.DataAnnotations;

namespace UralHiking.Models.Dto.Requests;

public sealed record CreateCommentRequest(
    [param:StringLength(256)] string Content,
    string UserLogin,
    [param:EmailAddress] string UserEmail
);