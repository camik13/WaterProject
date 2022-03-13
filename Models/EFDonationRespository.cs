using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterProject.Models
{
    public class EFDonationRespository : IDonationRepository
    {
        private WaterProjectContext context;
        public EFDonationRespository (WaterProjectContext temp)
        {
            context = temp;
        }
        public IQueryable<Donation> Donations => context.Donations
            .Include(x => x.Lines)
            .ThenInclude(x => x.Project);

        public void SaveDonation(Donation donation)
        {
            context.AttachRange(donation.Lines.Select(x => x.Project));

            if (donation.DonationID == 0)
            {
                context.Donations.Add(donation);
            }
            context.SaveChanges();
        }
    }
}
