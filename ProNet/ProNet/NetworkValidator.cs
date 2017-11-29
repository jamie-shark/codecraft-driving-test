using System;

namespace ProNet
{
    public class NetworkValidator : INetworkValidator
    {
        public void Validate(Network network)
        {
            if (network.Programmer == null)
                throw new ArgumentException("Network has no list of programmers");
        }
    }
}