namespace queueless.dto;

public record UserDto(
    string name,
    string username,
    string password,
    string role
);