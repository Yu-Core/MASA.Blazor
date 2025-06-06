﻿using Masa.Blazor.Components.DatePicker;

namespace Masa.Blazor.Presets;

public partial class PDateTimeCompactPickerView<TValue> : PDateTimePickerViewBase<TValue>
{
    [Inject]
    private I18n I18n { get; set; } = null!;

    [Parameter]
    public string? Transition { get; set; }

    /// <summary>
    /// Switch to time picker automatically when selecting a date
    /// </summary>
    [Parameter] [MasaApiParameter(defaultValue: true, releasedIn: "v1.6.0")]
    public bool AutoSwitchToTime { get; set; } = true;

    private const string DATE = "date";
    private const string TIME = "time";

    private StringNumber _tabValue = DATE;

    private MDatePicker<DateOnly?>? _datePicker;
    private MTimePicker? _timePicker;

    private TimePickerType _timeActivePicker = TimePickerType.Hour;

    private static Block TimePickerClockBlock => new("m-time-picker-clock"); 

    private string? DateTitle
    {
        get
        {
            if (_datePicker is not null && InternalDate.HasValue)
            {
                return DateFormatters.MonthDay(_datePicker.CurrentLocale)(InternalDate.Value);
            }

            return null;
        }
    }

    private string? YearTitle
    {
        get
        {
            if (_datePicker is not null && InternalDate.HasValue)
            {
                return DateFormatters.Year(_datePicker.CurrentLocale)(InternalDate.Value);
            }

            return null;
        }
    }

    private string? SelectingTime
    {
        get
        {
            switch (_timeActivePicker)
            {
                case TimePickerType.Hour:
                    return I18n.T("$masaBlazor.period.hour");
                case TimePickerType.Minute:
                    return I18n.T("$masaBlazor.period.minute");
                case TimePickerType.Second:
                    return I18n.T("$masaBlazor.period.second");
                default:
                    return null;
            }
        }
    }

    private int SupportedMaxTimePickerType
    {
        get
        {
            if (UseSeconds)
            {
                return 3;
            }

            return 2;
        }
    }

    private void OnPrevClick()
    {
        _timeActivePicker = (TimePickerType)(((int)_timeActivePicker) - 1);
    }

    private void OnNextClick()
    {
        _timeActivePicker = (TimePickerType)(((int)_timeActivePicker) + 1);
    }

    private void HandleOnDateClick(DateOnly dateOnly)
    {
        if (!AutoSwitchToTime)
        {
            return;
        }

        _tabValue = TIME;
    }
}
