using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tree.Contracts
{
    public enum SortDirection
    {
        Asc,
        Desc,
    }

    [DataContract(Namespace = "tree")]
    public class PaginationDto<T>
    {
        [DataMember]
        public long Page { get; set; }
        [DataMember]
        public long PageSize { get; set; }
        [DataMember]
        public string Sort { get; set; }
        [DataMember]
        public string SortDir { get; set; }

        [DataMember]
        public long Count { get; set; }
        [DataMember]
        public T[] Items { get; set; }

        public PaginationDto(long page, long pageSize, string sort, string sortDir, long count, T[] items) {
            Page = page;
            PageSize = pageSize;
            Sort = sort;
            SortDir = sortDir;
            Count = count;
            Items = items;
        }
    }
}
