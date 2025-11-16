using System.ComponentModel.DataAnnotations;

namespace UralHiking.Models.Dto.Requests;

public sealed record CreateCommentRequest(
    [property:Required]
    [property:StringLength(256)]
    string Content,

    [property:Required]
    string UserLogin,

    [property:Required]
    [property:EmailAddress]
    string UserEmail
);