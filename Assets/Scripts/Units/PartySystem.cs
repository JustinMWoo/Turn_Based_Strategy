using System.Collections.Generic;
using UnityEngine;

    public class PartySystem : MonoBehaviour
    {
        [SerializeField] List<Unit> party;

        public void Add(Unit unit)
        {
            party.Add(unit);
        }

        public void Remove(Unit unit)
        {
            party.Remove(unit);
        }

        public List<Unit> GetParty()
        {
            return party;
        }
    }

