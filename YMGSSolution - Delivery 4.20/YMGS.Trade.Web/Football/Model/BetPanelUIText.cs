using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Trade.Web.Common;

namespace YMGS.Trade.Web.Football.Model
{
    public class BetPanelUIText
    {
        public string Bestlip
        {
            get
            {
                return LangManager.GetString("Bestlip");
            }
        }

        public string BackBetFor
        {
            get
            {
                return LangManager.GetString("buy");
            }
        }

        public string Odds
        {
            get
            {
                return LangManager.GetString("odds");
            }
        }

        public string Stake
        {
            get
            {
                return LangManager.GetString("amount");
            }
        }

        public string Profit
        {
            get
            {
                return LangManager.GetString("profit");
            }
        }

        public string LayBetAgainst
        {
            get
            {
                return LangManager.GetString("Sale");
            }
        }

        public string BackerOdds
        {
            get
            {
                return LangManager.GetString("buyodds");
            }
        }

        public string BackerStake
        {
            get
            {
                return LangManager.GetString("buyamount");
            }
        }

        public string YourLiability
        {
            get
            {
                return LangManager.GetString("stage");
            }
        }

        public string Liability
        {
            get
            {
                return LangManager.GetString("Liability");
            }
        }

        public string PlaceBets
        {
            get
            {
                return LangManager.GetString("PlaceBets");
            }
        }

        public string CancelAllSelection
        {
            get
            {
                return LangManager.GetString("CancelAllSelection");
            }
        }

        public string ConfrimBet
        {
            get 
            {
                return LangManager.GetString("ConfrimBet");
            }
        }
    }
}