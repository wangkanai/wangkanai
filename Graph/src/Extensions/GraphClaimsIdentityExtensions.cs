// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Graph.Extensions;

public static class GraphClaimsIdentityExtensions
{
    // Adds claims from the provided User object
    public static void AddUserGraphInfo(this ClaimsIdentity identity, User user)
    {
        identity.IfNullThrow();

        var email     = new Claim(GraphClaimTypes.Email, user.Mail          ?? user.UserPrincipalName ?? "");
        var jobTitle  = new Claim(GraphClaimTypes.JobTitle, user.JobTitle   ?? "");
        var display   = new Claim(GraphClaimTypes.Display, user.DisplayName ?? "");
        var mobile    = new Claim(GraphClaimTypes.Mobile, user.MobilePhone  ?? "");
        var firstname = new Claim(GraphClaimTypes.Firstname, user.GivenName ?? "");
        var surname   = new Claim(GraphClaimTypes.Surname, user.Surname     ?? "");

        identity.AddClaim(email);
        identity.AddClaim(jobTitle);
        identity.AddClaim(display);
        identity.AddClaim(mobile);
        identity.AddClaim(firstname);
        identity.AddClaim(surname);
    }

    // Converts a photo Stream to a Data URI and stores it in a claim
    public static void AddUserGraphPhoto(this ClaimsIdentity identity, Stream photoStream)
    {
        identity.IfNullThrow();
        photoStream.IfNullThrow();

        // Copy the photo stream to a memory stream
        // to get the bytes out of it
        var memoryStream = new MemoryStream();
        photoStream.CopyTo(memoryStream);
        var photoBytes = memoryStream.ToArray();

        // Generate a date URI for the photo
        var photoUri = $"data:image/png;base64,{Convert.ToBase64String(photoBytes)}";

        identity.AddClaim(new Claim(GraphClaimTypes.Photo, photoUri));
    }
}