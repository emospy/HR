using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Data.Common;

namespace DataLayer
{
    public enum TransactionComnmand
    {
        BEGIN_TRANSACTION = 1,
        USE_TRANSACTION,
        COMMIT_TRANSACTION,
        ROLLBACK_TRANSACION,
        NO_TRANSACTION,
    }
}
