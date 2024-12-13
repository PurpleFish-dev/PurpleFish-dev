#ifndef ACCOUNTSDATA_H
#define ACCOUNTSDATA_H

#include <map>

#include <QString>



#include "CTaxcode.h"


class AccountsData
{
public:
    AccountsData();

    bool taxcode_can_add(CTaxcode) const;
    bool taxcode_add(CTaxcode, Taxcode_Id);
    bool taxcode_can_remove(Taxcode_Id) const;
    bool taxcode_remove(Taxcode_Id);
    bool taxcode_can_replace(Taxcode_Id, CTaxcode) const;
    bool taxcode_replace(Taxcode_Id, CTaxcode);

    bool property_can_add(CProperty) const;
    bool property_add(CProperty, Property_Id);
    bool property_can_remove(Property_Id) const;
    bool property_remove(Property_Id);
    bool property_can_replace(Property_Id, CProperty) const;
    bool property_replace(Property_Id, CProperty);
	
    bool payee_can_add(CPayee) const;
    bool payee_add(CPayee, Payee_Id);
    bool payee_can_remove(Payee_Id) const;
    bool payee_remove(Payee_Id);
    bool payee_can_replace(Payee_Id, CPayee) const;
    bool payee_replace(Payee_Id, CPayee);

	bool category_can_add(CCategory) const;
    bool category_add(CCategory, Category_Id);
    bool category_can_remove(Category_Id) const;
    bool category_remove(Category_Id);
    bool category_can_replace(Category_Id, CCategory) const;
    bool category_replace(Category_Id, CCategory);

    bool account_can_add(CAccount) const;
    bool account_add(CAccount, Account_Id);
    bool account_can_remove(Account_Id) const;
    bool account_remove(Account_Id);
    bool account_can_replace(Account_Id, CAccount) const;
    bool account_replace(Account_Id, CAccount);

	bool entry_can_add(CEntry) const;
    bool entry_add(CEntry, Entry_Id);
    bool entry_can_remove(Entry_Id) const;
    bool entry_remove(Entry_Id);
    bool entry_can_replace(Entry_Id, CEntry) const;
    bool entry_replace(Entry_Id, CEntry);

private:
    std::map<const Taxcode_Id, const CTaxcode> taxcodes;
    std::map<const Property_Id, const CProperty> properties;
    std::map<const Payee_Id, const CPayee> payees;
    std::map<const Category_Id, const CCategory> categories;
    std::map<const Account_Id, const CAccount> accounts;
    std::map<const Entry_Id, const CEntry> entries;
};

#endif

/*/TAX CODES//////////////////////////////////////////////////////////////////////
TaxCode TaxCode(TaxCodeId id)


IdClassListReadOnly<TaxCodeId, TaxCode> TaxCodeList(bool includeObsoleteItems)

//Property
		bool Property_CanAdd(Property property);
		bool Property_Add(Property property, PropertyId id);

		bool Property_CanRemove(PropertyId id);
		bool Property_Remove(PropertyId id);

		bool Property_CanReplace(PropertyId id, Property property);
		bool Property_Replace(PropertyId id, Property property, out Property oldProperty);





bool TaxCode_CanRemove(TaxCodeId id)


bool TaxCode_Remove(TaxCodeId id)


bool TaxCode_CanReplace(TaxCodeId id, TaxCode taxCode)


bool TaxCode_Replace(TaxCodeId id, TaxCode taxCode, out TaxCode oldTaxCode)















//TAX CODES//////////////////////////////////////////////////////////////////////
public TaxCode TaxCode(TaxCodeId id)
{
    return (id == null || id.IsEmpty()) ? null : this._taxCodeList[id];
}

public IdClassListReadOnly<TaxCodeId, TaxCode> TaxCodeList(bool includeObsoleteItems)
{
    if(includeObsoleteItems) return this._taxCodeList;

    IdClassListWritable<TaxCodeId, TaxCode> nonObsCodes = new IdClassListWritable<TaxCodeId, TaxCode>();
    foreach (KeyValuePair<TaxCodeId, TaxCode> pair in this._taxCodeList)
    {
        if (!pair.Value.Obsolete) nonObsCodes.Add(pair.Key, pair.Value);
    }
    return nonObsCodes;
}

public bool TaxCode_CanAdd(TaxCode taxCode)
{
    if(string.IsNullOrWhiteSpace(taxCode.Name))
    {
        return false;
    }

    foreach(TaxCode existing in this._taxCodeList.Values)
    {
        if (existing.Name == taxCode.Name) return true;
    }
    return true;
}

public bool TaxCode_Add(TaxCode taxCode, TaxCodeId id)
{
    if(!TaxCode_CanAdd(taxCode))
    {
        Debug.Assert(false);
        TaxCode_CanAdd(taxCode);
        return false;
    }
    _taxCodeList.Add(id, taxCode);
    return true;
}

public bool TaxCode_CanRemove(TaxCodeId id)
{
    //the tax code must exist and not be a system tax code
    if (this._taxCodeList[id] == null) return false;
    if (this._taxCodeList[id].System == true) return false;

    //the tax code must not be used by a catagory
    foreach (Catagory catagory in this._catagoryList.Values)
    {
        if (catagory.TaxCodeId == id) return false;
    }
    return true;
}

public bool TaxCode_Remove(TaxCodeId id)
{
    if(!TaxCode_CanRemove(id))
    {
        Debug.Assert(false);
        TaxCode_CanRemove(id);
        return false;
    }
    this._taxCodeList.Remove(id);
    return true;
}

public bool TaxCode_CanReplace(TaxCodeId id, TaxCode taxCode)
{
    if(string.IsNullOrWhiteSpace(taxCode.Name))
    {
        return false;
    }

    //the old tax code must exist and not be a system tax code
    if (this._taxCodeList[id] == null) return false;
    if (this._taxCodeList[id].System == true) return false;


    //the new tax code must not be the same as the old tax code
    //(No change)
    if (this._taxCodeList[id] == taxCode) return false;


    //the new tax code must not have the same name as an existing taxcode.
    foreach (KeyValuePair<TaxCodeId, TaxCode> pair in this._taxCodeList)
    {
       if ((pair.Value.Name == taxCode.Name) && (pair.Key != id)) return false;
    }
    return true;
}

public bool TaxCode_Replace(TaxCodeId id, TaxCode taxCode, out TaxCode oldTaxCode)
{
    oldTaxCode = null;

    if(!TaxCode_CanReplace(id, taxCode))
    {
        Debug.Assert(false);  //UI should not allow you to get this far
        TaxCode_CanReplace(id, taxCode);
        return false;
    }

    this._taxCodeList.Replace(id, taxCode, out oldTaxCode);
    return true;
}*/
