using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AAEICS.Client.Messages;

public class LanguageChangedMessage(string code) : ValueChangedMessage<string>(code);