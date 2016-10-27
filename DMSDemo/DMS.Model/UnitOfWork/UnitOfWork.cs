using DMS.Model.DMSModel;
using DMS.Model.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model.UnitOfWork
{
    /// <summary>
    /// UnitOfWork
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        #region Private member variables...

        /// <summary>
        /// The _context
        /// </summary>
        private DMSEntities _context = null;

        /// <summary>
        /// The _mercer job repository
        /// </summary>
        private GenericRepository<DocumentDetail> _documentDetailRepository;

        /// <summary>
        /// The _look up master repository
        /// </summary>
        private GenericRepository<LookupMaster> _lookUpMasterRepository;

        /// <summary>
        /// The _look up detail repository
        /// </summary>
        private GenericRepository<LookupDetail> _lookUpDetailRepository;

        /// <summary>
        /// The _user login repository
        /// </summary>
        private GenericRepository<UserLogin> _userLoginRepository;

        /// <summary>
        /// The _skill detail repository
        /// </summary>
        private GenericRepository<SkillDetail> _skillDetailRepository;

        /// <summary>
        /// The _token repository
        /// </summary>
        private GenericRepository<Token> _tokenRepository;

        #endregion

        #region private dispose variable declaration...
        /// <summary>
        /// The _disposed
        /// </summary>
        private bool _disposed = false;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            _context = new DMSEntities();
        }

        #region Public Repository Creation properties..

        /// <summary>
        /// Gets the merit guidance repository.
        /// </summary>
        /// <value>
        /// The merit guidance repository.
        /// </value>
        public GenericRepository<LookupDetail> LookupDetailRepository
        {
            get
            {
                if (this._lookUpDetailRepository == null)
                {
                    this._lookUpDetailRepository = new GenericRepository<LookupDetail>(_context);
                }

                return _lookUpDetailRepository;
            }
        }

        /// <summary>
        /// Gets the look up master repository.
        /// </summary>
        /// <value>
        /// The look up master repository.
        /// </value>
        public GenericRepository<LookupMaster> LookUpMasterRepository
        {
            get
            {
                if (this._lookUpMasterRepository == null)
                {
                    this._lookUpMasterRepository = new GenericRepository<LookupMaster>(_context);
                }

                return _lookUpMasterRepository;
            }
        }

        /// <summary>
        /// Gets the document detail repository.
        /// </summary>
        /// <value>
        /// The document detail repository.
        /// </value>
        public GenericRepository<DocumentDetail> DocumentDetailRepository
        {
            get
            {
                if (this._documentDetailRepository == null)
                {
                    this._documentDetailRepository = new GenericRepository<DocumentDetail>(_context);
                }

                return _documentDetailRepository;
            }
        }

        /// <summary>
        /// Gets the user login repository.
        /// </summary>
        /// <value>
        /// The user login repository.
        /// </value>
        public GenericRepository<UserLogin> UserLoginRepository
        {
            get
            {
                if (this._userLoginRepository == null)
                {
                    this._userLoginRepository = new GenericRepository<UserLogin>(_context);
                }

                return _userLoginRepository;
            }
        }

        /// <summary>
        /// Gets the skill detail repository.
        /// </summary>
        /// <value>
        /// The skill detail repository.
        /// </value>
        public GenericRepository<SkillDetail> SkillDetailRepository
        {
            get
            {
                if (this._skillDetailRepository == null)
                {
                    this._skillDetailRepository = new GenericRepository<SkillDetail>(_context);
                }

                return _skillDetailRepository;
            }
        }

        /// <summary>
        /// Gets the token repository.
        /// </summary>
        /// <value>
        /// The token repository.
        /// </value>
        public GenericRepository<Token> TokenRepository
        {
            get
            {
                if (this._tokenRepository == null)
                {
                    this._tokenRepository = new GenericRepository<Token>(_context);
                }

                return _tokenRepository;
            }
        }
        #endregion

        #region Public member methods...

        /// <summary>
        /// SQLs the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>TEntity</returns>
        public IEnumerable<TEntity> SQLQuery<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return _context.Database.SqlQuery<TEntity>(sql, parameters);
        }

        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }

                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                throw e;
            }
        }

        #endregion

        #region Implementing IDiosposable...

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }

            this._disposed = true;
        }
        #endregion
    }
}
