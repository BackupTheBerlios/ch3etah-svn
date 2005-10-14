using System;
using System.EnterpriseServices;

namespace Adapdev.Transactions
{
	/// <summary>
	/// Summary description for TransactionScope.
	/// </summary>
	public class TransactionScope : IDisposable
	{
		// Dispose must be called to exit the transactional block
		public void Dispose()
		{ 
			if(this.EnterSucceeded)
			{              
				if(!this.Consistent && !this.HasAborted)
				{
					ContextUtil.SetAbort();
				}
				ServiceDomain.Leave();
			}
		}

		// by calling this method, you mark the scope as being consistent
		// and ready to for commit
		// if the method is never called, upon dispose, the scope will abort the transaction
		public void Complete()
		{
			this.Consistent = true;
		}

		public void Abort()
		{
			ContextUtil.SetAbort();
			this.HasAborted = true;
		}

		public Guid TransactionId
		{
			get{return ContextUtil.TransactionId;}
		}

		public Guid ApplicationId
		{
			get{return ContextUtil.ApplicationId;}
		}
 
		public TransactionScope()
		{                
			EnterTxContext(TransactionOption.Required);
		}
 
		public TransactionScope(TransactionOption txOption)
		{
			EnterTxContext(txOption);
		}
 
		private void EnterTxContext(TransactionOption txOption)
		{
			ServiceConfig config = new ServiceConfig();
			config.Transaction = txOption;
			ServiceDomain.Enter(config);
			// Since Enter can throw, the next statement will track the success
			// In the case of success will we need to call Leave in Dispose
			this.EnterSucceeded = true;          
		}
 
		// By default, the scope is inconsistent;
		// To Commit the transaction on exit, the Consistent flag
		// must be set to true before Dispose is called
		private bool Consistent = false;

		// Enter can throw, so we need to know if we need to call Leave in Dispose
		private bool EnterSucceeded = false;

		// Track whether it's been aborted
		private bool HasAborted = false;
	}
}
