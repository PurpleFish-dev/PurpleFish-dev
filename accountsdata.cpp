#include "accountsdata.h"
#include "qdebug.h"


#include "cTaxCode.h"

AccountsData::AccountsData()
{

}

bool AccountsData::taxcode_can_add(CTaxCode taxCode) const
{
	auto it = std::find_if( taxcodes.begin(), taxcodes.end(), [&taxCode](const auto& pair) { return pair.second.name == taxCode.name; } );
	return !taxCode.name.isEmpty() && it == taxcodes.end();
}

/*
bool AccountsData::taxcode_can_add(CTaxCode taxCode)
{
    //QString name = taxCode.getName();


    auto i = std::find_if(
        _taxCodes.begin(),
        _taxCodes.end(),
        [&taxCode](const auto& pair){
            return pair.second.getName() == taxCode.getName();
        }
    );




    //struct find_by_name
    //{
    //    find_by_name(const QString& color) : color(color) {}
    //    bool operator()(const CTaxCode & car)
     //   {
     //       return car.getName() == color;
     //   }
    //private:
    //    QString color;
    //};

    //std::map<TaxCode_Id, CTaxCode>::const_iterator result = std::find_if(_taxCodes.begin(), _taxCodes.end(), find_by_name(taxCode.getName()));
    return !taxCode.getName().isEmpty() && (i != _taxCodes.end());



    //name exists
    //todoif(_taxCodes. )
    return true;
}
*/

bool AccountsData::taxcode_add(const CTaxCode taxcode, TaxCode_Id id)
{
	if(!taxcode_can_add(taxcode))
    {
        //Debug.Assert(false);
        //TaxCode_CanAdd(taxCode);
        //todo
        return false;
    }
	//taxcodes.insert([id] = taxcode;

			taxcodes.emplace(id, taxcode);
    return true;
}

bool AccountsData::taxcode_can_remove(TaxCode_Id id)
{
	//the tax code must exist
	if (taxcodes.find(id) == taxcodes.end())
		return false;

	//the tax code must not be used by a catagory
	if (const auto it = std::find_if(categories.begin(), categories.end(), [&id](const auto& pair) { return pair.second.taxcode_id == id; }) != categories.end())
		return false;

	return true;
}

bool AccountsData::property_can_add(CProperty property) const
{
	if(property.name.isEmpty())
        return false;

    //name exists
    //todoif(_taxCodes. )
    return true;
}

bool AccountsData::property_add(CProperty property, Property_Id id)
{
    if(!property_can_add(property))
    {
        //Debug.Assert(false);
        //TaxCode_CanAdd(taxCode);
        //todo
        return false;
    }
	properties.emplace(id, property);//[id] = property;
    return true;
}

bool AccountsData::payee_can_add(CPayee payee) const
{
//    struct find_by_name
//    {
//        find_by_name(const QString& name) : name(name) {}
//        bool operator()(const CPayee& tc) { return tc.getName() == name; }
//    private:
//        QString name;
//    };

//     auto it = std::find_if(_payees.begin(), _payees.end(), find_by_name(payee.getName()));
//    return !payee.getName().isEmpty() && (it == _payees.end());
	return true;
}

bool AccountsData::payee_add(CPayee payee, Payee_Id id)
{
    if(!payee_can_add(payee))
    {
        //Debug.Assert(false);
        //TaxCode_CanAdd(taxCode);
        //todo
        return false;
    }
	payees.emplace(id, payee);//[id] = payee;
    return true;
}

bool AccountsData::category_can_add(CCategory category) const
{
    struct find_by_name
    {
        find_by_name(const QString& name) : name(name) {}
		bool operator()(const std::pair<Category_Id, CCategory>& pair) { return pair.second.name == name; }
    private:
        QString name;
    };

	auto it = std::find_if(categories.begin(), categories.end(), find_by_name(category.name));
	return !category.name.isEmpty() && (it == categories.end());
}

bool AccountsData::category_add(CCategory cat, Category_Id id)
{
	if (category_can_add(cat)) {
		categories.emplace(id, cat);//[id] = cat;
		return true;
	}
	return false;
}

bool AccountsData::account_can_add(CAccount acc) const
{
	auto it = std::find_if( accounts.begin(), accounts.end(), [&acc](const auto& pair) { return pair.second.name == acc.name; } );
	return !acc.name.isEmpty() && it == accounts.end();
}

bool AccountsData::account_add(CAccount acc, Account_Id id)
{
	if (account_can_add(acc)) {
		accounts.emplace(id, acc);
		return true;
	}
	return false;
}

bool AccountsData::entry_can_add(CEntry entry) const
{
	return true;
	std::vector<QString> reasons;

	if (entry.account_id.isNull())
	{
		reasons.push_back("No account specified");
	}
	else if (!accounts.count(entry.account_id))
	{
		reasons.push_back("Account does not exist");
	}

	if (reasons.size() > 0)
		return false;

	//locked or reconciled account reasons
	//CAccount account = accounts.at(entry.account_id);
	if (accounts.at(entry.account_id).lock == eAccLock::AccLock::Locked)
		reasons.push_back("Account is locked, ");
	else if ((accounts.at(entry.account_id).lock == eAccLock::AccLock::LockedUntil) && (entry.date < accounts.at(entry.account_id).locked_until))
		reasons.push_back("Account is locked on the given date, ");
	if (entry.date <= accounts.at(entry.account_id).reconciled_on)
		reasons.push_back("Account is reconciled on the given date");

	//if this is a transfer
	if(!entry.transfer_id.isNull())
	{
		if (!accounts.count(entry.transfer_id))
			reasons.push_back("Account does not exist");

		//locked or reconciled account reasons
		const CAccount account = accounts.at(entry.transfer_id);
		if (account.lock == eAccLock::AccLock::Locked)
			reasons.push_back("Account is locked, ");
		else if ((account.lock == eAccLock::AccLock::LockedUntil) && (entry.date < account.locked_until))
			reasons.push_back("Account is locked on the given date, ");
		if (entry.date <= account.reconciled_on)
			reasons.push_back("Account is reconciled on the given date");
	}

	return reasons.size() > 0 ? false : true;
}

bool AccountsData::entry_add(CEntry entry, Entry_Id id)
{
	if (entry_can_add(entry)) {
		entries.emplace(id, entry);
		return true;
	}
	return false;
}
