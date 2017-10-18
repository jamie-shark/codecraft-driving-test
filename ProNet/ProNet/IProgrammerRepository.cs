using System.Collections.Generic;

namespace ProNet
{
    public interface IProgrammerRepository
    {
        List<Programmer> GetAll();
    }
}