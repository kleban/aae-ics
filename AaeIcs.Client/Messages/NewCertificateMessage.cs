using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AAEICS.Client.Messages;

public class NewCertificateMessage(bool isSuccessful) : ValueChangedMessage<bool>(isSuccessful);